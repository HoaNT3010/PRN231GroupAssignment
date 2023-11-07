using Infrastructure.Common;
using System.Linq.Expressions;

namespace Infrastructure.Repositories.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(object id);
        Task AddAsync(T entity);
        Task<T?> AddReturnEntityAsync(T entity);
        Task AddManyAsync(IEnumerable<T> entities);
        void UpdateAsync(T entity);
        Task DeleteByAsync(object id);
        Task<bool> ExistByIdAsync(object id);
        Task<IEnumerable<T>> GetAsync(
            Expression<Func<T, bool>>? filter,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy,
            string includeProperties,
            bool disableTracking = false);
        Task<PagedList<T>> GetPaginatedAsync(
            int pageSize,
            int pageNumber,
            Expression<Func<T, bool>>? filter,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy,
            string includeProperties,
            bool disableTracking = false);
        Task<PagedList<T>> GetPaginatedAsync(
            IQueryable<T> query,
            int pageSize,
            int pageNumber,
            Expression<Func<T, bool>>? filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            string includeProperties = "",
            bool disableTracking = false);
    }
}
