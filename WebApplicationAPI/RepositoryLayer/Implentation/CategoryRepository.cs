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
    public class CategoryRepository : IBaseRepository<Category>
    {
        private readonly ApplicationDbContext context;
        public CategoryRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public bool Add(Category model)
        {
            context.Categories.Add(model);
            int RowCount = context.SaveChanges();
            return RowCount > 0;
        }

        public bool Delete(Category model)
        {
            context.Categories.Remove(model);
            int RowCount = context.SaveChanges();
            return RowCount > 0;
        }

        public List<Category> FindList(Expression<Func<Category, bool>> predicate, string? incudes)
        {
            if (incudes == null)
            {
                return context.Categories.Where(predicate).ToList();
            }
            else
            {
                var query = context.Categories.Include(incudes).Where(predicate).AsQueryable();
                return query.ToList();
            }
        }

        public Category? FindOne(Expression<Func<Category, bool>> predicate, string? incudes)
        {
            if (incudes != null)
            {
                return context.Categories.Include(incudes).Where(predicate).AsNoTracking().FirstOrDefault();
            }
            else
            {
                return context.Categories.Where(predicate).AsNoTracking().FirstOrDefault();
            }
        }

        public List<Category> Get()
        {
            return context.Categories.ToList();
        }

        public List<Category> List(Expression<Func<Category, bool>> predicate, int page, int pageSize, string? incudes = null)
        {
            if (incudes != null)
            {
                var Query = context.Categories.Include(incudes).Where(predicate).AsQueryable();
                return Query.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            }
            else
            {
                var Query = context.Categories.Where(predicate).AsQueryable();
                return Query.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            }
        }

        public bool Update(Category model)
        {
            context.Categories.Update(model);
            int RowCount = context.SaveChanges();
            return RowCount > 0;
        }
    }
}
