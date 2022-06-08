using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Model
{
    public class UserRole
    {
        public int UserId { get; set; }
        public User Users { get;  set; }
        public int RoleId { get;  set; }
        public Role Roles { get;  set; }
    }
}
