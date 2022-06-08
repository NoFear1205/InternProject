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
        public BaseService(IBaseRepository<T> BaseRepository)
        {
            this.baseRepository = BaseRepository;
        }
        public bool Add(T Model)
        {
            return baseRepository.Add(Model);
        }

        public bool Delete(T Model)
        {
            return baseRepository.Delete(Model);
        }

        public List<T> FindList(Expression<Func<T, bool>> Predicate,string? Incudes)
        {
            return baseRepository.FindList(Predicate, Incudes); 
        }

        public T? FindOne(Expression<Func<T, bool>> Predicate, string? Incudes = null)
        {
            return (T?)baseRepository.FindOne(Predicate, Incudes);
        }

        public List<T> Get()
        {
            return baseRepository.Get();
        }

        public List<T> List(Expression<Func<T, bool>> Predicate, int Page, int PageSize, string? Incudes = null)
        {
           return baseRepository.List(Predicate, Page, PageSize, Incudes);
        }

        public bool Update(T Model)
        {
            return baseRepository.Update(Model);
        }
    }
}
