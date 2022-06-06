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
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Username).IsRequired().HasMaxLength(30);
            builder.HasIndex(c => c.Username).IsUnique();
            builder.Property(c => c.PasswordHash).IsRequired();
            builder.Property(c => c.PasswordSalt).IsRequired();
        }
    }
}
