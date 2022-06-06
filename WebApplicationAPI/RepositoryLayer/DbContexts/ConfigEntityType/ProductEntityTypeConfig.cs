using DomainLayer.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.DbContextLayer
{
    internal class ProductEntityTypeConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id);
            builder
                .Property(p=>p.Name)
                .HasMaxLength(30).IsRequired();
            builder
             .Property(p => p.Provider)
             .HasMaxLength(100).IsRequired();
            builder.HasOne<Category>(c => c.Category)
                   .WithMany(p => p.Products)
                   .OnDelete(DeleteBehavior.Restrict)
                   .HasForeignKey(c => c.CategoryID);
        }
    }
}
