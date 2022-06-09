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
        string CreateToken(List<Claim> claims);
        RefreshToken GenerateRefreshToken(int userId);
        void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
        bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt);
        //RefreshToken GetRefreshToken(int userId);
        bool UpdateRefreshToken(RefreshToken model);
        bool AddRefreshToken(RefreshToken model);
        public ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token);
    }
}
