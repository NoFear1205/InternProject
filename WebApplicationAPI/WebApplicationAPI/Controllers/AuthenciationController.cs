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
        public AuthenciationController(IBaseService<User> UserService, IAuthenService AuthenService, IBaseService<UserRole> UserRolerService, IBaseService<RefreshToken> RefreshTokenService)
        {
            this.userService = UserService;
            this.authenService = AuthenService;
            this.userRolerService = UserRolerService;
            this.refreshTokenService = RefreshTokenService;
        }
        [HttpPost]
        [Route("Login")]
        public IActionResult Login([FromBody] LoginRequestModel Model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var User = userService.FindOne(c => c.Username.Contains(Model.Username));
            if (User == null)
            {
                ModelState.AddModelError("Username", "Username không tồn tại");
                return BadRequest(ModelState);
            }
            else if (!authenService.VerifyPasswordHash(Model.Password, User.PasswordHash, User.PasswordSalt))
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
        public IActionResult Register([FromBody] RegisterRequestModel Model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else if (Model.RePassword != Model.Password)
            {
                ModelState.AddModelError("RePassword", "Nhập lại mật khẩu không đúng");
                return BadRequest(ModelState);
            }
            else if (userService.FindOne(c => c.Username == Model.Username) != null)
            {
                ModelState.AddModelError("Username", "Username đã tồn tại");
                return BadRequest(ModelState);
            }
            else
            {
                List<UserRole> ListUserRole = new List<UserRole>();
                foreach (var item in Model.Roles)
                {
                    ListUserRole.Add(new UserRole() { RoleId = item });
                }
                authenService.CreatePasswordHash(Model.Password, out byte[] PasswordHash, out byte[] PasswordSalt);
                var User = new User()
                {
                    Username = Model.Username,
                    PasswordHash = PasswordHash,
                    PasswordSalt = PasswordSalt,
                    User_Roles = ListUserRole
                };
                userService.Add(User);
                return Ok(User);
            }
        }
        [HttpPost]
        [Route ("Refresh-token")]
        public IActionResult RefreshToken([FromBody] LoginResponseModel Model)
        {
            var Claims = authenService.GetPrincipalFromExpiredToken(Model.AccessToken);
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
                } else if (!Refreshtoken.refreshToken.Equals(Model.RefreshToken))
                {
                    return BadRequest("Invalid refresh token");
                } else if(Refreshtoken.Expires < DateTime.UtcNow)
                {
                    return BadRequest("Expired time");
                }
                else
                {
                    return Ok(new LoginResponseModel() { AccessToken = authenService.CreateToken(Claims.Claims.ToList()),RefreshToken =Model.RefreshToken});
                }  
            }
        }
        [HttpPut]
        [Authorize]
        [Route ("Change-password")]
        public IActionResult ChangePassword([FromBody] ChangePasswordRequestModel Model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var Users = userService.FindOne(c => c.Username.Equals(User.Identity.Name));
            if (!authenService.VerifyPasswordHash(Model.OldPassword, Users.PasswordHash, Users.PasswordSalt))
            {
                ModelState.AddModelError("Password", "Mật khẩu không đúng");
                return BadRequest(ModelState);
            }
            else if (!Model.NewPassword.Equals(Model.RePassword))
            {
                ModelState.AddModelError("RePassword", "Nhập lại mật khẩu không đúng");
                return BadRequest(ModelState);
            }
            else
            {
                authenService.CreatePasswordHash(Model.NewPassword, out byte[] PasswordHash, out byte[] PasswordSalt);
                Users.PasswordHash=PasswordHash;
                Users.PasswordSalt=PasswordSalt;
                return Ok(userService.Update(Users));
            }

        }
    }
}
