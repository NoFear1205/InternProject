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
    public class ResfreshTokenEntityConfig : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.HasKey(sc => sc.id);

            builder
                .HasOne<User>(sc => sc.user)
                .WithMany(s => s.RefreshToken)
                .HasForeignKey(sc => sc.userID);
        }
    }
}
