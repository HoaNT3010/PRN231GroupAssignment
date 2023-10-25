using System.Linq.Expressions;

namespace Infrastructure.Repositories.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        Task<ICollection<T>> GetAllAsync(Func<IQueryable<T>, ICollection<T>> options = null, string includeProperties = null);
        Task<T> GetByIdAsync(Expression<Func<T, bool>> filter = null,
            string includeProperties = null);
        Task AddAsync(T entity);
        Task<T?> AddReturnEntityAsync(T entity);
        Task AddManyAsync(IEnumerable<T> entities);
        Task UpdateAsync(T entity);
        Task DeleteByAsync(object id);
        Task<bool> ExistByIdAsync(object id);

    }
}
