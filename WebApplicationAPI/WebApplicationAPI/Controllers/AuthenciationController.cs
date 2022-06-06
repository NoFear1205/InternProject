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
        private readonly IBaseService<User_Role> useRolerService;
        private readonly IBaseService<RefreshToken> RefreshTokenService;
        private readonly IAuthenService authenService;
        public AuthenciationController(IBaseService<User> userService, IAuthenService authenService, IBaseService<User_Role> useRolerService, IBaseService<RefreshToken> RefreshTokenService)
        {
            this.userService = userService;
            this.authenService = authenService;
            this.useRolerService = useRolerService;
            this.RefreshTokenService = RefreshTokenService;
        }
        [HttpPost]
        [Route("Login")]
        public IActionResult login([FromBody] LoginRequest model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var temp = userService.FindOne(c => c.Username.Contains(model.Username));
            if (temp == null)
            {
                ModelState.AddModelError("Username", "Username không tồn tại");
                return BadRequest(ModelState);
            }
            else if (!authenService.VerifyPasswordHash(model.Password, temp.PasswordHash, temp.PasswordSalt))
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
                var list = useRolerService.FindList(c => c.UserId == temp.Id, "Roles");
                foreach (var item in list)
                {
                    claims.Add(new Claim(ClaimTypes.Role, item.Roles.Name));
                };
                var refresh = authenService.GenerateRefreshToken(temp.Id);
                var oldRefresh = RefreshTokenService.FindOne(c => c.userID == temp.Id);
                if (oldRefresh == null)
                {
                    RefreshTokenService.Add(refresh);
                }
                else
                {
                    oldRefresh.refreshToken = refresh.refreshToken;
                    oldRefresh.Expires = refresh.Expires;
                    RefreshTokenService.Update(oldRefresh);
                }
                
                return Ok(new LoginResponse
                {
                    AccessToken = authenService.CreateToken(claims),
                    RefreshToken = refresh.refreshToken
                });
            }
        }
        [HttpPost]
        [Route("register")]
        public IActionResult Register([FromBody] RegisterRequest model)
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
                List<User_Role> list = new List<User_Role>();
                foreach (var item in model.Roles)
                {
                    list.Add(new User_Role() { RoleId = item });
                }
                authenService.CreatePasswordHash(model.Password, out byte[] passwordHash, out byte[] passwordSalt);
                var temp = new User()
                {
                    Username = model.Username,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                    user_Roles = list
                };
                userService.Add(temp);
                return Ok(temp);
            }
        }
        [HttpPost]
        [Route ("Refresh-token")]
        public IActionResult RefreshToken([FromBody] LoginResponse model)
        {
            var temp = authenService.GetPrincipalFromExpiredToken(model.AccessToken);
            var user = userService.FindOne(c => c.Username.Equals(temp.Identity.Name));
            if(user == null)
            {
                ModelState.AddModelError("Accesstoken", "Accesstoken error");
                return BadRequest(ModelState);
            }
            else 
            {
                var refreshtoken = RefreshTokenService.FindOne(c => c.userID == user.Id);
                if (refreshtoken == null)
                {
                    return BadRequest("Invalid refresh token");
                } else if (!refreshtoken.refreshToken.Equals(model.RefreshToken))
                {
                    return BadRequest("Invalid refresh token");
                } else if(refreshtoken.Expires < DateTime.UtcNow)
                {
                    return BadRequest("Expired time");
                }
                else
                {
                    return Ok(new LoginResponse() { AccessToken = authenService.CreateToken(temp.Claims.ToList()),RefreshToken =model.RefreshToken});
                }  
            }
        }
        [HttpPut]
        [Authorize]
        [Route ("Change-password")]
        public IActionResult RefreshToken([FromBody] ChangePasswordRequest model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var temp = userService.FindOne(c => c.Username.Equals(User.Identity.Name));
            if (!authenService.VerifyPasswordHash(model.oldPassword, temp.PasswordHash, temp.PasswordSalt))
            {
                ModelState.AddModelError("Password", "Mật khẩu không đúng");
                return BadRequest(ModelState);
            }
            else if (!model.newPassword.Equals(model.rePassword))
            {
                ModelState.AddModelError("RePassword", "Nhập lại mật khẩu không đúng");
                return BadRequest(ModelState);
            }
            else
            {
                authenService.CreatePasswordHash(model.newPassword, out byte[] passwordHash, out byte[] passwordSalt);
                temp.PasswordHash=passwordHash;
                temp.PasswordSalt=passwordSalt;
                return Ok(userService.Update(temp));
            }

        }
    }
}
