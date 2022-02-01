using System.Linq.Expressions;
using Core.Persistence.Paging;
using Microsoft.EntityFrameworkCore.Query;

namespace Core.Persistence.Repositories;

public interface IRepository<T> where T : Entity
{
    T Get(Expression<Func<T, bool>> predicate);

    IPaginate<T> GetList(Expression<Func<T, bool>>? predicate = null,
                         Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
                         Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
                         int index = 0,
                         int size = 10,
                         bool enableTracking = true);

    IQueryable<T> Query();
    T Add(T entity);
    void Update(T entity);
    void Delete(T entity);
}