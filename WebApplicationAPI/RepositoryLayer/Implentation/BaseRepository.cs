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
        private readonly ApplicationDbContext Context;
        private DbSet<T> Table;
        public BaseRepository(ApplicationDbContext Context)
        {
            this.Context = Context;
            Table = Context.Set<T>();
        }

        public bool Add(T Model)
        {
            Table.Add(Model);
            int row_Count = Context.SaveChanges();
            return row_Count > 0;
        }

        public bool Delete(T Model)
        {
            Table.Remove(Model);
            int RowCount = Context.SaveChanges();
            return RowCount > 0;
        }

        public List<T> FindList(Expression<Func<T, bool>> Predicate, string? Incudes = null)
        {
            if (Incudes == null)
            {
                return Table.Where(Predicate).ToList();
            }
            else
            {
                var query = Table.Include(Incudes).Where(Predicate).AsQueryable();
                return query.ToList();
            }
        }

        public T? FindOne(Expression<Func<T, bool>> Predicate, string Incudes)
        {
            if (Incudes != null)
            {
                return Table.Include(Incudes).Where(Predicate).AsNoTracking().FirstOrDefault();
            }
            else
            {
                return Table.Where(Predicate).AsNoTracking().FirstOrDefault();
            }

        }

        public List<T> Get()
        {
            return Table.ToList();
        }

        public List<T> List(Expression<Func<T, bool>> Predicate, int Page, int PageSize, string? Incudes = null)
        {
            if(Incudes != null)
            {
                var Query = Table.Include(Incudes).Where(Predicate).AsQueryable();
                return Query.Skip((Page - 1) * PageSize).Take(PageSize).ToList();
            }
            else
            {
                var Query = Table.Where(Predicate).AsQueryable();
                return Query.Skip((Page - 1) * PageSize).Take(PageSize).ToList();
            }
        }

        public bool Update(T Model)
        {
            Table.Update(Model);
            int RowCount = Context.SaveChanges();
            return RowCount > 0;
        }
    }
}
