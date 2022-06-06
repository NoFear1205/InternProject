using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Interfaces
{
    public interface IBaseService<T> where T : class
    {
        bool Add(T model);
        bool Update(T model);
        bool Delete(T model);
        List<T> Get();
        T? FindOne(Expression<Func<T, bool>> predicate, string? incudes = null);
        List<T> FindList(Expression<Func<T, bool>> predicate, string incudes);
        List<T> List(Expression<Func<T, bool>> predicate, int page, int pageSize, string? incudes = null);

    }
}
