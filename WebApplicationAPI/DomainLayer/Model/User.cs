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
        public string Username { get;  set; }
        public byte[] PasswordHash { get;  set; }
        public byte[] PasswordSalt { get;  set; }
        public List<User_Role>? user_Roles { get;  set; }
        public IList<RefreshToken>? RefreshToken { get;  set; }
    }
}
