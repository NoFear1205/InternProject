using DomainLayer.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.DbContexts.ConfigEntityType
{
    public class RoleEntityConfig : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> Builder)
        {
            Builder.HasKey(c => c.Id);
            Builder.Property(c => c.Name).IsRequired();
            Builder.HasIndex(c => c.Name).IsUnique();
        }
    }
}
