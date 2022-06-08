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
        public void SetId(int Id)
        {
            this.Id=Id;
        }
        public void SetUsername(string Username)
        {
            this.Username = Username;
        }
        public void SetPasswordHash(byte[] PasswordHash)
        { 
            this.PasswordHash = PasswordHash;
        }
        public void SetPasswordSalt(byte[] PasswordSalt)
        {
            this.PasswordSalt = PasswordSalt;
        }
        public void SetUserRoles(List<UserRole>? UserRoles)
        {
            this.UserRoles = UserRoles;
        }


    }
}
