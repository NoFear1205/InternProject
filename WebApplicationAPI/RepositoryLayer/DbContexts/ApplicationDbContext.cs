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
        protected override void OnModelCreating(ModelBuilder Builder)
        {
            //Config Entity role
            new RoleEntityConfig().Configure(Builder.Entity<Role>());
            
            //Config Entity User
            new UserEntityConfig().Configure(Builder.Entity<User>());
            
            //Config Entity Product
            new ProductEntityTypeConfig().Configure(Builder.Entity<Product>());
            
            //Config Entity Category
            new CategoryEntityConfig().Configure(Builder.Entity<Category>());

            //Config Entity User_Role
            new UserRoleEntityConfig().Configure(Builder.Entity<UserRole>());
        }                
        public DbSet<Role> Role { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<UserRole> User_Role { get; set; }
    }
}
