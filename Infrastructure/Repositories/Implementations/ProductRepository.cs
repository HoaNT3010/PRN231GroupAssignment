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
        public async Task<PagedList<Product>> GetProductList(ProductListParameters parameters)
        {
            var query = this.context.Set<Product>().Include(p => p.Category);
            var sortQuery = CheckSortProductQuery(parameters);
            var list = await GetPaginatedAsync(query, parameters.PageSize, parameters.PageNumber,null, sortQuery);

            return list;

        }
        public async override Task<Product?> GetByIdAsync(object id)
        {
            return await this.dbSet
                .Include(e => e.Category)
                .SingleOrDefaultAsync(e => e.Id == (int)id);
        }
        private Func<IQueryable<Product>, IOrderedQueryable<Product>> CheckSortProductQuery(ProductListParameters parameters)
        {
            switch (parameters.SortProduct)
            {
                case Domain.Enums.ProductSortOrder.NameDescending:
                    return (query => query.OrderByDescending(i => i.Name));
                case Domain.Enums.ProductSortOrder.NameAscending:
                    return (query => query.OrderBy(i => i.Name));
                case Domain.Enums.ProductSortOrder.PriceDescending:
                    return (query => query.OrderByDescending(i => i.UnitPrice));
                case Domain.Enums.ProductSortOrder.PriceAscending:
                    return (query => query.OrderBy(i => i.UnitPrice));
                default:
                    return (query => query.OrderByDescending(i => i.Name));
            }
        }
    }
}
