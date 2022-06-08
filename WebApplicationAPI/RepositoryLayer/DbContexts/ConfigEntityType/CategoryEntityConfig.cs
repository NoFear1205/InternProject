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
    public class CategoryEntityConfig : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> Builder)
        {
            Builder.HasKey(c=>c.Id);
            Builder.Property(c=>c.Name).HasMaxLength(30).IsRequired();
            //modelBuilder.Ignore(x => x.Products);
        }
    }
}
