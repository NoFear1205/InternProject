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
    public class UserRoleRepository : IBaseRepository<UserRole>
    {
        private readonly ApplicationDbContext context;
        public UserRoleRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public bool Add(UserRole model)
        {
            context.User_Role.Add(model);
            int RowCount = context.SaveChanges();
            return RowCount > 0;
        }

        public bool Delete(UserRole model)
        {
            context.User_Role.Remove(model);
            int RowCount = context.SaveChanges();
            return RowCount > 0;
        }

        public List<UserRole> FindList(Expression<Func<UserRole, bool>> predicate, string? incudes)
        {
            if (incudes == null)
            {
                return context.User_Role.Where(predicate).ToList();
            }
            else
            {
                var query = context.User_Role.Include(incudes).Where(predicate).AsQueryable();
                return query.ToList();
            }
        }

        public UserRole? FindOne(Expression<Func<UserRole, bool>> predicate, string? incudes)
        {
            if (incudes != null)
            {
                return context.User_Role.Include(incudes).Where(predicate).AsNoTracking().FirstOrDefault();
            }
            else
            {
                return context.User_Role.Where(predicate).AsNoTracking().FirstOrDefault();
            }
        }

        public List<UserRole> Get()
        {
            return context.User_Role.ToList();
        }

        public List<UserRole> List(Expression<Func<UserRole, bool>> predicate, int page, int pageSize, string? incudes = null)
        {
            if (incudes != null)
            {
                var Query = context.User_Role.Include(incudes).Where(predicate).AsQueryable();
                return Query.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            }
            else
            {
                var Query = context.User_Role.Where(predicate).AsQueryable();
                return Query.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            }
        }

        public bool Update(UserRole model)
        {
            context.User_Role.Update(model);
            int RowCount = context.SaveChanges();
            return RowCount > 0;
        }
    }
}
