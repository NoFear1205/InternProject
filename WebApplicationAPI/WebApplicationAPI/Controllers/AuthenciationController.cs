using DomainLayer.Model;
using DomainLayer.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Interfaces;
using System.Security.Claims;

namespace WebApplicationAPI.Controllers
{
    [ApiController]
    [Route("api")]
    public class AuthenciationController : Controller
    {
        private readonly IBaseService<User> userService;
        private readonly IBaseService<UserRole> userRolerService;
        private readonly IBaseService<RefreshToken> refreshTokenService;
        private readonly IAuthenService authenService;
        public AuthenciationController(IBaseService<User> userService, IAuthenService authenService, IBaseService<UserRole> userRolerService, IBaseService<RefreshToken> refreshTokenService)
        {
            this.userService = userService;
            this.authenService = authenService;
            this.userRolerService = userRolerService;
            this.refreshTokenService = refreshTokenService;
        }
        [HttpPost]
        [Route("Login")]
        public IActionResult Login([FromBody] LoginRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var User = userService.FindOne(c => c.Username.Contains(model.Username));
            if (User == null)
            {
                ModelState.AddModelError("Username", "Username không tồn tại");
                return BadRequest(ModelState);
            }
            else if (!authenService.VerifyPasswordHash(model.Password, User.PasswordHash, User.PasswordSalt))
            {
                ModelState.AddModelError("Password", "Password không đúng");
                return BadRequest(ModelState);
            }
            else
            {
                List<Claim> claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, User.Username)
                };
                var list = userRolerService.FindList(c => c.UserId == User.Id, "Roles");
                foreach (var item in list)
                {
                    claims.Add(new Claim(ClaimTypes.Role, item.Roles.Name));
                };
                var NewRefreshToken = authenService.GenerateRefreshToken(User.Id);
                var OldRefreshToken = refreshTokenService.FindOne(c => c.UserID == User.Id);
                if (OldRefreshToken == null)
                {
                    refreshTokenService.Add(NewRefreshToken);
                }
                else
                {
                    OldRefreshToken.refreshToken = NewRefreshToken.refreshToken;
                    OldRefreshToken.Expires = NewRefreshToken.Expires;
                    refreshTokenService.Update(OldRefreshToken);
                }
                
                return Ok(new LoginResponseModel
                {
                    AccessToken = authenService.CreateToken(claims),
                    RefreshToken = NewRefreshToken.refreshToken
                });
            }
        }
        [HttpPost]
        [Route("register")]
        public IActionResult Register([FromBody] RegisterRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else if (model.RePassword != model.Password)
            {
                ModelState.AddModelError("RePassword", "Nhập lại mật khẩu không đúng");
                return BadRequest(ModelState);
            }
            else if (userService.FindOne(c => c.Username == model.Username) != null)
            {
                ModelState.AddModelError("Username", "Username đã tồn tại");
                return BadRequest(ModelState);
            }
            else
            {
                List<UserRole> ListUserRole = new List<UserRole>();
                foreach (var item in model.Roles)
                {
                    ListUserRole.Add(new UserRole() { RoleId = item });
                }
                authenService.CreatePasswordHash(model.Password, out byte[] passwordHash, out byte[] passwordSalt);
                var User = new User();
                User.SetUsername(model.Username);
                User.SetPasswordHash(passwordHash);
                User.SetPasswordSalt(passwordSalt);
                User.SetUserRoles(ListUserRole);    
                userService.Add(User);
                return Ok(User);
            }
        }
        [HttpPost]
        [Route ("Refresh-token")]
        public IActionResult RefreshToken([FromBody] LoginResponseModel model)
        {
            var Claims = authenService.GetPrincipalFromExpiredToken(model.AccessToken);
            var User = userService.FindOne(c => c.Username.Equals(Claims.Identity.Name));
            if(User == null)
            {
                ModelState.AddModelError("Accesstoken", "Accesstoken error");
                return BadRequest(ModelState);
            }
            else 
            {
                var Refreshtoken = refreshTokenService.FindOne(c => c.UserID == User.Id);
                if (Refreshtoken == null)
                {
                    return BadRequest("Invalid refresh token");
                } else if (!Refreshtoken.refreshToken.Equals(model.RefreshToken))
                {
                    return BadRequest("Invalid refresh token");
                } else if(Refreshtoken.Expires < DateTime.UtcNow)
                {
                    return BadRequest("Expired time");
                }
                else
                {
                    return Ok(new LoginResponseModel() { AccessToken = authenService.CreateToken(Claims.Claims.ToList()),RefreshToken =model.RefreshToken});
                }  
            }
        }
        [HttpPut]
        [Authorize]
        [Route ("Change-password")]
        public IActionResult ChangePassword([FromBody] ChangePasswordRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var Users = userService.FindOne(c => c.Username.Equals(User.Identity.Name));
            if (!authenService.VerifyPasswordHash(model.OldPassword, Users.PasswordHash, Users.PasswordSalt))
            {
                ModelState.AddModelError("Password", "Mật khẩu không đúng");
                return BadRequest(ModelState);
            }
            else if (!model.NewPassword.Equals(model.RePassword))
            {
                ModelState.AddModelError("RePassword", "Nhập lại mật khẩu không đúng");
                return BadRequest(ModelState);
            }
            else
            {
                authenService.CreatePasswordHash(model.NewPassword, out byte[] PasswordHash, out byte[] PasswordSalt);
                Users.SetPasswordHash(PasswordHash);
                Users.SetPasswordSalt(PasswordSalt);
                return Ok(userService.Update(Users));
            }

        }
    }
}
