using Domain.Entities;
using Infrastructure.Repositories.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repositories.Implementations
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(StoreDbContext context) : base(context)
        {
        }
    }
}
