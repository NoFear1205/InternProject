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
    public class RefreshTokenRepository : IBaseRepository<RefreshToken>
    {
        private readonly ApplicationDbContext context;
        public RefreshTokenRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public bool Add(RefreshToken model)
        {
            context.RefreshTokens.Add(model);
            int RowCount = context.SaveChanges();
            return RowCount > 0;
        }

        public bool Delete(RefreshToken model)
        {
            context.RefreshTokens.Remove(model);
            int RowCount = context.SaveChanges();
            return RowCount > 0;
        }

        public List<RefreshToken> FindList(Expression<Func<RefreshToken, bool>> predicate, string? incudes)
        {
            if (incudes == null)
            {
                return context.RefreshTokens.Where(predicate).ToList();
            }
            else
            {
                var query = context.RefreshTokens.Include(incudes).Where(predicate).AsQueryable();
                return query.ToList();
            }
        }

        public RefreshToken? FindOne(Expression<Func<RefreshToken, bool>> predicate, string? incudes)
        {
            if (incudes != null)
            {
                return context.RefreshTokens.Include(incudes).Where(predicate).AsNoTracking().FirstOrDefault();
            }
            else
            {
                return context.RefreshTokens.Where(predicate).AsNoTracking().FirstOrDefault();
            }
        }

        public List<RefreshToken> Get()
        {
            return context.RefreshTokens.ToList();
        }

        public List<RefreshToken> List(Expression<Func<RefreshToken, bool>> predicate, int page, int pageSize, string? incudes = null)
        {
            if (incudes != null)
            {
                var Query = context.RefreshTokens.Include(incudes).Where(predicate).AsQueryable();
                return Query.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            }
            else
            {
                var Query = context.RefreshTokens.Where(predicate).AsQueryable();
                return Query.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            }
        }

        public bool Update(RefreshToken model)
        {
            context.RefreshTokens.Update(model);
            int RowCount = context.SaveChanges();
            return RowCount > 0;
        }
    }
}
