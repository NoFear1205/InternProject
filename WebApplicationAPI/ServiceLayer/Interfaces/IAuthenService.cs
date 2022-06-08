using DomainLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Interfaces
{
    public interface IAuthenService
    {
        string CreateToken(List<Claim> Claims);
        RefreshToken GenerateRefreshToken(int UserId);
        void CreatePasswordHash(string Password, out byte[] PasswordHash, out byte[] PasswordSalt);
        bool VerifyPasswordHash(string Password, byte[] PasswordHash, byte[] PasswordSalt);
        //RefreshToken GetRefreshToken(int userId);
        bool UpdateRefreshToken(RefreshToken Model);
        bool AddRefreshToken(RefreshToken Model);
        public ClaimsPrincipal? GetPrincipalFromExpiredToken(string? Token);
    }
}
