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
        bool Add(T Model);
        bool Update(T Model);
        bool Delete(T Model);
        List<T> Get();
        T? FindOne(Expression<Func<T, bool>> Predicate, string? Incudes = null);
        List<T> FindList(Expression<Func<T, bool>> Predicate, string Incudes);
        List<T> List(Expression<Func<T, bool>> Predicate, int Page, int PageSize, string? Incudes = null);

    }
}
