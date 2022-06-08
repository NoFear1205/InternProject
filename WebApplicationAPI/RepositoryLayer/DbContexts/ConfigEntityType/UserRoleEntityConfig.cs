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
        public void Configure(EntityTypeBuilder<UserRole> Builder)
        {
            Builder.HasKey(sc => new { sc.RoleId, sc.UserId });

            Builder
                .HasOne<User>(sc => sc.Users)
                .WithMany(s => s.UserRoles)
                .HasForeignKey(sc => sc.UserId);
            Builder
                .HasOne<Role>(sc => sc.Roles)
                .WithMany(s => s.UserRoles)
                .HasForeignKey(sc => sc.RoleId);
        }
    }
}
