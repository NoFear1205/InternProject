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
        public string CreateToken(List<Claim> claims)
        {
            var Key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                config.GetSection("AppSettings:Token").Value));

            var Credentials = new SigningCredentials(Key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddMinutes(1),
                signingCredentials: Credentials);
            var Token = new JwtSecurityTokenHandler().WriteToken(token);
            return Token;
        }
        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        public bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }
        public RefreshToken GenerateRefreshToken(int userId)
        {
            var RandomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(RandomNumber);
                return new RefreshToken()
                {
                    refreshToken = Convert.ToBase64String(RandomNumber),
                    UserID = userId,
                    Expires = DateTime.UtcNow.AddMinutes(10)
                };
            }
        }
        public bool AddRefreshToken(RefreshToken model)
        {
            return refresh.Add(model);
        }
        public bool UpdateRefreshToken(RefreshToken model)
        {
            return refresh.Update(model);
        }
        public ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token)
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
            Principal = TokenHandler.ValidateToken(token, TokenValidationParameters, out SecurityToken SecurityToken);
            return Principal;

        }
    }
}
