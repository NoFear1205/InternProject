using RepositoryLayer.Implentation;
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
    public class BaseService<T> : IBaseService<T> where T : class
    {
        private readonly IBaseRepository<T> baseRepository;
        public BaseService(IBaseRepository<T> baseRepository)
        {
            this.baseRepository = baseRepository;
        }
        public bool Add(T model)
        {
            return baseRepository.Add(model);
        }

        public bool Delete(T model)
        {
            return baseRepository.Delete(model);
        }

        public List<T> FindList(Expression<Func<T, bool>> predicate,string? incudes)
        {
            return baseRepository.FindList(predicate, incudes); 
        }

        public T? FindOne(Expression<Func<T, bool>> predicate, string? incudes = null)
        {
            return (T?)baseRepository.FindOne(predicate, incudes);
        }

        public List<T> Get()
        {
            return baseRepository.Get();
        }

        public List<T> List(Expression<Func<T, bool>> predicate, int page, int pageSize, string? incudes = null)
        {
           return baseRepository.List(predicate, page, pageSize, incudes);
        }

        public bool Update(T model)
        {
            return baseRepository.Update(model);
        }
    }
}
