using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Model
{
    public class Role
    {
        public int Id{get; private set;}
        public string Name { get; private set; }
        public Role(int id,string name)
        {
            Id = id;
            Name = name;
        }
        public List<UserRole>? User_Roles { get; private set; }
    }
}
