namespace Domain.Interfaces.Repositories
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

    }
}
