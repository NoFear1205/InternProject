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
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly ApplicationDbContext context;
        private DbSet<T> table;
        public BaseRepository(ApplicationDbContext context)
        {
            this.context = context;
            table = context.Set<T>();
        }

        public bool Add(T model)
        {
            table.Add(model);
            int RowCount = context.SaveChanges();
            return RowCount > 0;
        }

        public bool Delete(T model)
        {
            table.Remove(model);
            int RowCount = context.SaveChanges();
            return RowCount > 0;
        }

        public List<T> FindList(Expression<Func<T, bool>> predicate, string? incudes = null)
        {
            if (incudes == null)
            {
                return table.Where(predicate).ToList();
            }
            else
            {
                var query = table.Include(incudes).Where(predicate).AsQueryable();
                return query.ToList();
            }
        }

        public T? FindOne(Expression<Func<T, bool>> predicate, string incudes)
        {
            if (incudes != null)
            {
                return table.Include(incudes).Where(predicate).AsNoTracking().FirstOrDefault();
            }
            else
            {
                return table.Where(predicate).AsNoTracking().FirstOrDefault();
            }

        }

        public List<T> Get()
        {
            return table.ToList();
        }

        public List<T> List(Expression<Func<T, bool>> predicate, int page, int pageSize, string? incudes = null)
        {
            if(incudes != null)
            {
                var Query = table.Include(incudes).Where(predicate).AsQueryable();
                return Query.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            }
            else
            {
                var Query = table.Where(predicate).AsQueryable();
                return Query.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            }
        }

        public bool Update(T model)
        {
            table.Update(model);
            int RowCount = context.SaveChanges();
            return RowCount > 0;
        }
    }
}
