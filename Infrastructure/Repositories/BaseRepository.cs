using Domain.Interfaces.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Infrastructure.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly StoreDbContext context;
        protected readonly DbSet<T> dbSet;

        public BaseRepository(StoreDbContext context)
        {
            this.context = context;
            dbSet = context.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
            await dbSet.AddAsync(entity);
        }

        public async Task AddManyAsync(IEnumerable<T> entities)
        {
            await dbSet.AddRangeAsync(entities);
        }

        public async Task<T?> AddReturnEntityAsync(T entity)
        {
            EntityEntry<T> resultEntity = await dbSet.AddAsync(entity);
            return resultEntity.Entity;
        }

        public async Task DeleteByAsync(object id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                dbSet.Remove(entity);
            }
        }

        public async Task<bool> ExistByIdAsync(object id)
        {
            return await GetByIdAsync(id) != null;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await dbSet.ToListAsync();
        }

        public async Task<T?> GetByIdAsync(object id)
        {
            return await dbSet.FindAsync(id);
        }

        public void UpdateAsync(T entity)
        {
            dbSet.Update(entity);
        }
    }
}
