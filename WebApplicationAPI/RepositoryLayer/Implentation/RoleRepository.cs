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
    public class RoleRepository : IBaseRepository<Role>
    {
        private readonly ApplicationDbContext context;
        public RoleRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public bool Add(Role model)
        {
            context.Role.Add(model);
            int RowCount = context.SaveChanges();
            return RowCount > 0;
        }

        public bool Delete(Role model)
        {
            context.Role.Remove(model);
            int RowCount = context.SaveChanges();
            return RowCount > 0;
        }

        public List<Role> FindList(Expression<Func<Role, bool>> predicate, string? incudes)
        {
            if (incudes == null)
            {
                return context.Role.Where(predicate).ToList();
            }
            else
            {
                var query = context.Role.Include(incudes).Where(predicate).AsQueryable();
                return query.ToList();
            }
        }

        public Role? FindOne(Expression<Func<Role, bool>> predicate, string? incudes)
        {
            if (incudes != null)
            {
                return context.Role.Include(incudes).Where(predicate).AsNoTracking().FirstOrDefault();
            }
            else
            {
                return context.Role.Where(predicate).AsNoTracking().FirstOrDefault();
            }
        }

        public List<Role> Get()
        {
            return context.Role.ToList();
        }

        public List<Role> List(Expression<Func<Role, bool>> predicate, int page, int pageSize, string? incudes = null)
        {
            if (incudes != null)
            {
                var Query = context.Role.Include(incudes).Where(predicate).AsQueryable();
                return Query.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            }
            else
            {
                var Query = context.Role.Where(predicate).AsQueryable();
                return Query.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            }
        }

        public bool Update(Role model)
        {
            context.Role.Update(model);
            int RowCount = context.SaveChanges();
            return RowCount > 0;
        }
    }
}
