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
        public BaseRepository(ApplicationDbContext Context)
        {
            this.context = Context;
            table = Context.Set<T>();
        }

        public bool Add(T Model)
        {
            table.Add(Model);
            int row_Count = context.SaveChanges();
            return row_Count > 0;
        }

        public bool Delete(T Model)
        {
            table.Remove(Model);
            int RowCount = context.SaveChanges();
            return RowCount > 0;
        }

        public List<T> FindList(Expression<Func<T, bool>> Predicate, string? Incudes = null)
        {
            if (Incudes == null)
            {
                return table.Where(Predicate).ToList();
            }
            else
            {
                var query = table.Include(Incudes).Where(Predicate).AsQueryable();
                return query.ToList();
            }
        }

        public T? FindOne(Expression<Func<T, bool>> Predicate, string Incudes)
        {
            if (Incudes != null)
            {
                return table.Include(Incudes).Where(Predicate).AsNoTracking().FirstOrDefault();
            }
            else
            {
                return table.Where(Predicate).AsNoTracking().FirstOrDefault();
            }

        }

        public List<T> Get()
        {
            return table.ToList();
        }

        public List<T> List(Expression<Func<T, bool>> Predicate, int Page, int PageSize, string? Incudes = null)
        {
            if(Incudes != null)
            {
                var Query = table.Include(Incudes).Where(Predicate).AsQueryable();
                return Query.Skip((Page - 1) * PageSize).Take(PageSize).ToList();
            }
            else
            {
                var Query = table.Where(Predicate).AsQueryable();
                return Query.Skip((Page - 1) * PageSize).Take(PageSize).ToList();
            }
        }

        public bool Update(T Model)
        {
            table.Update(Model);
            int RowCount = context.SaveChanges();
            return RowCount > 0;
        }
    }
}
