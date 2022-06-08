using DomainLayer.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Interfaces;
using ServiceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Implentation
{
    public class AuthenciationService : IAuthenService
    {
        private readonly IConfiguration config;
        private readonly IBaseRepository<RefreshToken> refresh;

        public AuthenciationService(IConfiguration config, IBaseRepository<RefreshToken> refresh)
        {
            this.config = config;
            this.refresh = refresh;
        }
        public string CreateToken(List<Claim> Claims)
        {
            var Key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                config.GetSection("AppSettings:Token").Value));

            var Credentials = new SigningCredentials(Key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: Claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddMinutes(1),
                signingCredentials: Credentials);
            var Token = new JwtSecurityTokenHandler().WriteToken(token);
            return Token;
        }
        public void CreatePasswordHash(string Password, out byte[] PasswordHash, out byte[] PasswordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                PasswordSalt = hmac.Key;
                PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(Password));
            }
        }
        public bool VerifyPasswordHash(string Password, byte[] PasswordHash, byte[] PasswordSalt)
        {
            using (var hmac = new HMACSHA512(PasswordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(Password));
                return computedHash.SequenceEqual(PasswordHash);
            }
        }
        public RefreshToken GenerateRefreshToken(int UserId)
        {
            var RandomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(RandomNumber);
                return new RefreshToken()
                {
                    refreshToken = Convert.ToBase64String(RandomNumber),
                    UserID = UserId,
                    Expires = DateTime.UtcNow.AddMinutes(10)
                };
            }
        }
        public bool AddRefreshToken(RefreshToken Model)
        {
            return refresh.Add(Model);
        }
        public bool UpdateRefreshToken(RefreshToken Model)
        {
            return refresh.Update(Model);
        }
        public ClaimsPrincipal? GetPrincipalFromExpiredToken(string? Token)
        {
            ClaimsPrincipal Principal;
            var TokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.GetSection("AppSettings:Token").Value)),
                ValidateLifetime = false
            };

            var TokenHandler = new JwtSecurityTokenHandler();
            Principal = TokenHandler.ValidateToken(Token, TokenValidationParameters, out SecurityToken SecurityToken);
            return Principal;

        }
    }
}
