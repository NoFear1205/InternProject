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
        private readonly IBaseService<User> UserService;
        private readonly IBaseService<User_Role> UserRolerService;
        private readonly IBaseService<RefreshToken> RefreshTokenService;
        private readonly IAuthenService AuthenService;
        public AuthenciationController(IBaseService<User> UserService, IAuthenService AuthenService, IBaseService<User_Role> UserRolerService, IBaseService<RefreshToken> RefreshTokenService)
        {
            this.UserService = UserService;
            this.AuthenService = AuthenService;
            this.UserRolerService = UserRolerService;
            this.RefreshTokenService = RefreshTokenService;
        }
        [HttpPost]
        [Route("Login")]
        public IActionResult Login([FromBody] LoginRequest Model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var temp = UserService.FindOne(c => c.Username.Contains(Model.Username));
            if (temp == null)
            {
                ModelState.AddModelError("Username", "Username không tồn tại");
                return BadRequest(ModelState);
            }
            else if (!AuthenService.VerifyPasswordHash(Model.Password, temp.PasswordHash, temp.PasswordSalt))
            {
                ModelState.AddModelError("Password", "Password không đúng");
                return BadRequest(ModelState);
            }
            else
            {
                List<Claim> claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, temp.Username)
                };
                var list = UserRolerService.FindList(c => c.UserId == temp.Id, "Roles");
                foreach (var item in list)
                {
                    claims.Add(new Claim(ClaimTypes.Role, item.Roles.Name));
                };
                var refresh = AuthenService.GenerateRefreshToken(temp.Id);
                var OldRefreshToken = RefreshTokenService.FindOne(c => c.UserID == temp.Id);
                if (OldRefreshToken == null)
                {
                    RefreshTokenService.Add(refresh);
                }
                else
                {
                    OldRefreshToken.refreshToken = refresh.refreshToken;
                    OldRefreshToken.Expires = refresh.Expires;
                    RefreshTokenService.Update(OldRefreshToken);
                }
                
                return Ok(new LoginResponse
                {
                    AccessToken = AuthenService.CreateToken(claims),
                    RefreshToken = refresh.refreshToken
                });
            }
        }
        [HttpPost]
        [Route("register")]
        public IActionResult Register([FromBody] RegisterRequest Model)
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
            else if (UserService.FindOne(c => c.Username == Model.Username) != null)
            {
                ModelState.AddModelError("Username", "Username đã tồn tại");
                return BadRequest(ModelState);
            }
            else
            {
                List<User_Role> list = new List<User_Role>();
                foreach (var item in Model.Roles)
                {
                    list.Add(new User_Role() { RoleId = item });
                }
                AuthenService.CreatePasswordHash(Model.Password, out byte[] PasswordHash, out byte[] PasswordSalt);
                var User = new User()
                {
                    Username = Model.Username,
                    PasswordHash = PasswordHash,
                    PasswordSalt = PasswordSalt,
                    User_Roles = list
                };
                UserService.Add(User);
                return Ok(User);
            }
        }
        [HttpPost]
        [Route ("Refresh-token")]
        public IActionResult RefreshToken([FromBody] LoginResponse Model)
        {
            var Claims = AuthenService.GetPrincipalFromExpiredToken(Model.AccessToken);
            var User = UserService.FindOne(c => c.Username.Equals(Claims.Identity.Name));
            if(User == null)
            {
                ModelState.AddModelError("Accesstoken", "Accesstoken error");
                return BadRequest(ModelState);
            }
            else 
            {
                var Refreshtoken = RefreshTokenService.FindOne(c => c.UserID == User.Id);
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
                    return Ok(new LoginResponse() { AccessToken = AuthenService.CreateToken(Claims.Claims.ToList()),RefreshToken =Model.RefreshToken});
                }  
            }
        }
        [HttpPut]
        [Authorize]
        [Route ("Change-password")]
        public IActionResult ChangePassword([FromBody] ChangePasswordRequest Model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var Users = UserService.FindOne(c => c.Username.Equals(User.Identity.Name));
            if (!AuthenService.VerifyPasswordHash(Model.OldPassword, Users.PasswordHash, Users.PasswordSalt))
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
                AuthenService.CreatePasswordHash(Model.NewPassword, out byte[] PasswordHash, out byte[] PasswordSalt);
                Users.PasswordHash=PasswordHash;
                Users.PasswordSalt=PasswordSalt;
                return Ok(UserService.Update(Users));
            }

        }
    }
}
