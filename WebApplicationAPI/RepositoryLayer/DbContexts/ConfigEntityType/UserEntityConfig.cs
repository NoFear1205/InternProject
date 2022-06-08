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
    public class UserEntityConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> Builder)
        {
            Builder.HasKey(c => c.Id);
            Builder.Property(c => c.Username).IsRequired().HasMaxLength(30);
            Builder.HasIndex(c => c.Username).IsUnique();
            Builder.Property(c => c.PasswordHash).IsRequired();
            Builder.Property(c => c.PasswordSalt).IsRequired();
        }
    }
}
