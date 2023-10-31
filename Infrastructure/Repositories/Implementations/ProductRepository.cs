using Domain.Entities;
using Infrastructure.Repositories.Interfaces;
using Infrastructure.Data;
using Infrastructure.Common;
using Infrastructure.Common.Parameters;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Implementations
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(StoreDbContext context) : base(context)
        {
        }
        public async Task<PagedList<Product>> GetProductList(QueryStringParameters parameters)
        {
            var query = this.context.Set<Product>().Include(p => p.Category);
            var list = await GetPaginatedAsync(parameters.PageSize, parameters.PageNumber);

            return list;

        }
        public async Task<Product?> GetByIdAsync(object id)
        {
            return await dbSet
                .Include(e => e.Category)
                .SingleOrDefaultAsync(e => e.Id == (int)id);
        }
    }
}
