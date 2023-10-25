using Infrastructure.Repositories.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq.Expressions;

namespace Infrastructure.Repositories.Implementations
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
            await context.SaveChangesAsync();
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
            var entity = await dbSet.FindAsync(id);
            if (entity != null)
            {
                dbSet.Remove(entity);
                await context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistByIdAsync(object id)
        {
            return await dbSet.FindAsync(id) != null;
        }

        public async Task<ICollection<T>> GetAllAsync(Func<IQueryable<T>, ICollection<T>> options = null, string includeProperties = null)
        {
            try
            {
                IQueryable<T> query = dbSet;

                if (includeProperties != null)
                {
                    foreach (var includeProp in includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        query = query.Include(includeProp);
                    }
                }

                if (options != null)
                {
                    return options(query).ToList();
                }

                return await query.ToListAsync();
            }
            catch
            {
                Console.WriteLine("Error");
            }
            return null;
        }

        public async Task<T?> GetByIdAsync(Expression<Func<T, bool>> filter = null, string includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }

            return await query.FirstOrDefaultAsync();
        }

        public Task UpdateAsync(T entity)
        {
            dbSet.Update(entity);
           return context.SaveChangesAsync();

        }
    }
}
