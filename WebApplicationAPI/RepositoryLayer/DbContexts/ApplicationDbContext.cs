using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Model;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.DbContextLayer;
using RepositoryLayer.DbContexts.ConfigEntityType;

namespace RepositoryLayer.DbContexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions con) : base(con)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Config Entity role
            new RoleEntityConfig().Configure(modelBuilder.Entity<Role>());
            //Config Entity User
            new UserEntityConfig().Configure(modelBuilder.Entity<User>());
            //Config Entity Product
            new ProductEntityTypeConfig().Configure(modelBuilder.Entity<Product>());
            //Config Entity Category
            new CategoryEntityConfig().Configure(modelBuilder.Entity<Category>());
            //Config Entity User_Role
            new UserRoleEntityConfig().Configure(modelBuilder.Entity<User_Role>());
        }                
        public DbSet<Role> Role { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User_Role> User_Role { get; set; }
    }
}
