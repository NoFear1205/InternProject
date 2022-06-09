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
    public class UserRepository : IBaseRepository<User>
    {
        private readonly ApplicationDbContext context;
        public UserRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public bool Add(User model)
        {
            context.Users.Add(model);
            int RowCount = context.SaveChanges();
            return RowCount > 0;
        }

        public bool Delete(User model)
        {
            context.Users.Remove(model);
            int RowCount = context.SaveChanges();
            return RowCount > 0;
        }

        public List<User> FindList(Expression<Func<User, bool>> predicate, string? incudes)
        {
            if (incudes == null)
            {
                return context.Users.Where(predicate).ToList();
            }
            else
            {
                var query = context.Users.Include(incudes).Where(predicate).AsQueryable();
                return query.ToList();
            }
        }

        public User? FindOne(Expression<Func<User, bool>> predicate, string? incudes)
        {
            if (incudes != null)
            {
                return context.Users.Include(incudes).Where(predicate).AsNoTracking().FirstOrDefault();
            }
            else
            {
                return context.Users.Where(predicate).AsNoTracking().FirstOrDefault();
            }
        }

        public List<User> Get()
        {
            return context.Users.ToList();
        }

        public List<User> List(Expression<Func<User, bool>> predicate, int page, int pageSize, string? incudes = null)
        {
            if (incudes != null)
            {
                var Query = context.Users.Include(incudes).Where(predicate).AsQueryable();
                return Query.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            }
            else
            {
                var Query = context.Users.Where(predicate).AsQueryable();
                return Query.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            }
        }

        public bool Update(User model)
        {
            context.Users.Update(model);
            int RowCount = context.SaveChanges();
            return RowCount > 0;
        }
    }
}
