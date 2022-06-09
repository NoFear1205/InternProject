using DomainLayer.Model;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.DbContexts;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Implentation
{
    public class ProductRepository : IBaseRepository<Product>
    {
        private readonly ApplicationDbContext context;
        public ProductRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public bool Add(Product model)
        {
            context.Products.Add(model);
            int RowCount = context.SaveChanges();
            return RowCount > 0;
        }

        public bool Delete(Product model)
        {
            context.Products.Remove(model);
            int RowCount = context.SaveChanges();
            return RowCount > 0;
        }

        public List<Product> FindList(Expression<Func<Product, bool>> predicate, string? incudes)
        {
            if (incudes == null)
            {
                return context.Products.Where(predicate).ToList();
            }
            else
            {
                var query = context.Products.Include(incudes).Where(predicate).AsQueryable();
                return query.ToList();
            }
        }

        public Product? FindOne(Expression<Func<Product, bool>> predicate, string? incudes)
        {
            if (incudes != null)
            {
                return context.Products.Include(incudes).Where(predicate).AsNoTracking().FirstOrDefault();
            }
            else
            {
                return context.Products.Where(predicate).AsNoTracking().FirstOrDefault();
            }
        }

        public List<Product> Get()
        {
            return context.Products.ToList();
        }

        public List<Product> List(Expression<Func<Product, bool>> predicate, int page, int pageSize, string? incudes = null)
        {
            if (incudes != null)
            {
                var Query = context.Products.Include(incudes).Where(predicate).AsQueryable();
                return Query.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            }
            else
            {
                var Query = context.Products.Where(predicate).AsQueryable();
                return Query.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            }
        }

        public bool Update(Product model)
        {
            context.Products.Update(model);
            int RowCount = context.SaveChanges();
            return RowCount > 0;
        }
    }
}
