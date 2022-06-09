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
    public class CategoryService : IBaseService<Category>
    {
        private readonly IBaseRepository<Category> baseRepository;
         public CategoryService(IBaseRepository<Category> baseRepository)
        {
            this.baseRepository = baseRepository;
        }
        public bool Add(Category model)
        {
            return baseRepository.Add(model);
        }

        public bool Delete(Category model)
        {
            return baseRepository.Delete(model);
        }

        public List<Category> FindList(Expression<Func<Category, bool>> predicate, string incudes)
        {
            return baseRepository.FindList(predicate, incudes);
        }

        public Category? FindOne(Expression<Func<Category, bool>> predicate, string? incudes = null)
        {
            return baseRepository.FindOne(predicate, incudes);
        }

        public List<Category> Get()
        {
            return baseRepository.Get();
        }

        public List<Category> List(Expression<Func<Category, bool>> predicate, int page, int pageSize, string? incudes = null)
        {
            return baseRepository.List(predicate, page, pageSize, incudes);
        }

        public bool Update(Category model)
        {
            return baseRepository.Update(model);
        }
    }
}
