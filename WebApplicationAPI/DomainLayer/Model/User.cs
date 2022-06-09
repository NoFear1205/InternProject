using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Model
{
    public class User
    {
        public int Id { get;  set; }
        public string Username { get; private set; }
        public byte[] PasswordHash { get; private set; }
        public byte[] PasswordSalt { get; private set; }
        public List<UserRole>? UserRoles { get; private set; }
        public IList<RefreshToken>? RefreshToken { get; private set; }
        public User() { }
        public void SetId(int id)
        {
            this.Id=id;
        }
        public void SetUsername(string username)
        {
            this.Username = username;
        }
        public void SetPasswordHash(byte[] passwordHash)
        { 
            this.PasswordHash = passwordHash;
        }
        public void SetPasswordSalt(byte[] passwordSalt)
        {
            this.PasswordSalt = passwordSalt;
        }
        public void SetUserRoles(List<UserRole>? userRoles)
        {
            this.UserRoles = userRoles;
        }


    }
}
