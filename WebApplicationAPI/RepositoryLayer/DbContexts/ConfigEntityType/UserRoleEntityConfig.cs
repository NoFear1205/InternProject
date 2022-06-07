using DomainLayer.Model;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.DbContexts.ConfigEntityType
{
    public class UserRoleEntityConfig
    {
        public void Configure(EntityTypeBuilder<User_Role> builder)
        {
            builder.HasKey(sc => new { sc.RoleId, sc.UserId });

            builder
                .HasOne<User>(sc => sc.Users)
                .WithMany(s => s.User_Roles)
                .HasForeignKey(sc => sc.UserId);
            builder
                .HasOne<Role>(sc => sc.Roles)
                .WithMany(s => s.User_Roles)
                .HasForeignKey(sc => sc.RoleId);
        }
    }
}
