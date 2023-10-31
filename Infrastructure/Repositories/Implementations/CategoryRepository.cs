using Domain.Entities;
using Infrastructure.Repositories.Interfaces;
using Infrastructure.Data;
using Infrastructure.Common.Parameters;
using Infrastructure.Common;

namespace Infrastructure.Repositories.Implementations
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(StoreDbContext context) : base(context)
        {
        }
        public async Task<PagedList<Category>> GetCategorList(QueryStringParameters parameters)
        {
            return await GetPaginatedAsync(parameters.PageSize, parameters.PageNumber);
        }
    }
}
