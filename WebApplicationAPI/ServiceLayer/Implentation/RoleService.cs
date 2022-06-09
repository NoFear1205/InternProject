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
    public class RoleService : IBaseService<Role>
    {
        private readonly IBaseRepository<Role> baseRepository;
        public RoleService(IBaseRepository<Role> baseRepository)
        {
            this.baseRepository = baseRepository;
        }
        public bool Add(Role model)
        {
            return baseRepository.Add(model);
        }

        public bool Delete(Role model)
        {
            return baseRepository.Delete(model);
        }

        public List<Role> FindList(Expression<Func<Role, bool>> predicate, string incudes)
        {
            return baseRepository.FindList(predicate, incudes);
        }

        public Role? FindOne(Expression<Func<Role, bool>> predicate, string? incudes = null)
        {
            return baseRepository.FindOne(predicate, incudes);
        }

        public List<Role> Get()
        {
            return baseRepository.Get();
        }

        public List<Role> List(Expression<Func<Role, bool>> predicate, int page, int pageSize, string? incudes = null)
        {
            return baseRepository.List(predicate, page, pageSize, incudes);
        }

        public bool Update(Role model)
        {
            return baseRepository.Update(model);
        }
    }
}
