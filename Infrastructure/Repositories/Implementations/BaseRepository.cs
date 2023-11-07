using Infrastructure.Repositories.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;
using Infrastructure.Common;

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
        public virtual async Task<T?> GetByIdAsync(object id)
        {
            return await dbSet.FindAsync(id);
        }

        public void UpdateAsync(T entity)
        {
            dbSet.Update(entity);
        }

        public virtual async Task<IEnumerable<T>> GetAsync(
            Expression<Func<T, bool>>? filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            string includeProperties = "",
            bool disableTracking = false)
        {
            IQueryable<T> query = dbSet;
            try
            {
                if (disableTracking)
                {
                    query = query.AsNoTracking();
                }

                if (filter != null)
                {
                    query = query.Where(filter);
                }

                query = includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

                if (orderBy != null)
                {
                    return await orderBy(query).ToListAsync();
                }
                else
                {
                    return await query.ToListAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error when trying to query data for entity {nameof(T)}");
            }
        }

        public virtual async Task<PagedList<T>> GetPaginatedAsync(
            int pageSize,
            int pageNumber,
            Expression<Func<T, bool>>? filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            string includeProperties = "",
            bool disableTracking = false)
        {
            IQueryable<T> query = dbSet;
            try
            {
                if (disableTracking)
                {
                    query = query.AsNoTracking();
                }

                if (filter != null)
                {
                    query = query.Where(filter);
                }

                query = includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

                int totalCount = await query.CountAsync();

                if (orderBy != null)
                {
                    query = orderBy(query);
                }

                query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);
                return new PagedList<T>(await query.ToListAsync(), totalCount, pageNumber, pageSize);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error when trying to query paginated data for entity {nameof(T)}");
            }
        }
        public virtual async Task<PagedList<T>> GetPaginatedAsync(
            IQueryable<T> query,
            int pageSize,
            int pageNumber,
            Expression<Func<T, bool>>? filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            string includeProperties = "",
            bool disableTracking = false)
        {
            try
            {
                if (disableTracking)
                {
                    query = query.AsNoTracking();
                }

                if (filter != null)
                {
                    query = query.Where(filter);
                }

                query = includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

                int totalCount = await query.CountAsync();

                if (orderBy != null)
                {
                    query = orderBy(query);
                }

                query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);
                return new PagedList<T>(await query.ToListAsync(), totalCount, pageNumber, pageSize);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error when trying to query paginated data for entity {nameof(T)}");
            }
        }
    }
}
