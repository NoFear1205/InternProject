using DomainLayer.Model;
using RepositoryLayer.Interfaces;
using ServiceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Implentation
{
    public class RefreshTokenService : IBaseService<RefreshToken>
    {
        private readonly IBaseRepository<RefreshToken> baseRepository;
        public RefreshTokenService(IBaseRepository<RefreshToken> baseRepository)
        {
            this.baseRepository = baseRepository;
        }
        public bool Add(RefreshToken model)
        {
            return baseRepository.Add(model);
        }

        public bool Delete(RefreshToken model)
        {
            return baseRepository.Delete(model);
        }

        public List<RefreshToken> FindList(Expression<Func<RefreshToken, bool>> predicate, string incudes)
        {
            return baseRepository.FindList(predicate, incudes);
        }

        public RefreshToken? FindOne(Expression<Func<RefreshToken, bool>> predicate, string? incudes = null)
        {
            return baseRepository.FindOne(predicate, incudes);
        }

        public List<RefreshToken> Get()
        {
            return baseRepository.Get();
        }

        public List<RefreshToken> List(Expression<Func<RefreshToken, bool>> predicate, int page, int pageSize, string? incudes = null)
        {
            return baseRepository.List(predicate, page, pageSize, incudes);
        }

        public bool Update(RefreshToken model)
        {
            return baseRepository.Update(model);
        }
    }
}
