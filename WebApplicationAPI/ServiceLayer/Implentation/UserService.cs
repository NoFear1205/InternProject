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
    public class UserService : IBaseService<User>
    {
        private readonly IBaseRepository<User> baseRepository;
        public UserService(IBaseRepository<User> baseRepository)
        {
            this.baseRepository = baseRepository;
        }
        public bool Add(User model)
        {
            return baseRepository.Add(model);
        }

        public bool Delete(User model)
        {
            return baseRepository.Delete(model);
        }

        public List<User> FindList(Expression<Func<User, bool>> predicate, string incudes)
        {
            return baseRepository.FindList(predicate, incudes);
        }

        public User? FindOne(Expression<Func<User, bool>> predicate, string? incudes = null)
        {
            return baseRepository.FindOne(predicate, incudes);
        }

        public List<User> Get()
        {
            return baseRepository.Get();
        }

        public List<User> List(Expression<Func<User, bool>> predicate, int page, int pageSize, string? incudes = null)
        {
            return baseRepository.List(predicate, page, pageSize, incudes);
        }

        public bool Update(User model)
        {
            return baseRepository.Update(model);
        }
    }
}
