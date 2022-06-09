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
    public class UserRoleService : IBaseService<UserRole>
    {
        private readonly IBaseRepository<UserRole> baseRepository;
        public UserRoleService(IBaseRepository<UserRole> baseRepository)
        {
            this.baseRepository = baseRepository;
        }
        public bool Add(UserRole model)
        {
            return baseRepository.Add(model);
        }

        public bool Delete(UserRole model)
        {
            return baseRepository.Delete(model);
        }

        public List<UserRole> FindList(Expression<Func<UserRole, bool>> predicate, string incudes)
        {
            return baseRepository.FindList(predicate, incudes);
        }

        public UserRole? FindOne(Expression<Func<UserRole, bool>> predicate, string? incudes = null)
        {
            return baseRepository.FindOne(predicate, incudes);
        }

        public List<UserRole> Get()
        {
            return baseRepository.Get();
        }

        public List<UserRole> List(Expression<Func<UserRole, bool>> predicate, int page, int pageSize, string? incudes = null)
        {
            return baseRepository.List(predicate, page, pageSize, incudes);
        }

        public bool Update(UserRole model)
        {
            return baseRepository.Update(model);
        }
    }
}
