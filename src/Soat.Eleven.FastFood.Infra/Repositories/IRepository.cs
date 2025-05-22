using System.Linq.Expressions;

namespace Soat.Eleven.FastFood.Infra.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<T> AddAsync(T entity);
        Task<T?> GetByIdAsync(Guid id, params Expression<Func<T, object>>[] includes);
        Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includes);
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IQueryable<T>>? include = null);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<T> SaveBatchAsync(T masterEntity, params Expression<Func<T, IEnumerable<object>>>[] detailSelectors);
        IQueryable<T> Queryable();
        Task DeleteRangeAsync(List<T> entities);
        Task AddRangeAsync(List<T> entities);
    }
}
