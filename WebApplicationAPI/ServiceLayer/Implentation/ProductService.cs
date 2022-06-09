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
    public class ProductService : IBaseService<Product>
    {
        private IBaseRepository<Product> baseRepository;
        public ProductService(IBaseRepository<Product> baseRepository)
        {
            this.baseRepository = baseRepository;
        }
        public bool Add(Product model)
        {
            return baseRepository.Add(model);
        }

        public bool Delete(Product model)
        {
            return baseRepository.Delete(model);
        }

        public List<Product> FindList(Expression<Func<Product, bool>> predicate, string incudes)
        {
            return baseRepository.FindList(predicate, incudes);
        }

        public Product? FindOne(Expression<Func<Product, bool>> predicate, string? incudes = null)
        {
            return baseRepository.FindOne(predicate, incudes);
        }

        public List<Product> Get()
        {
            return baseRepository.Get();
        }

        public List<Product> List(Expression<Func<Product, bool>> predicate, int page, int pageSize, string? incudes = null)
        {
            return baseRepository.List(predicate, page, pageSize, incudes);
        }

        public bool Update(Product model)
        {
            return baseRepository.Update(model);
        }
    }
}
