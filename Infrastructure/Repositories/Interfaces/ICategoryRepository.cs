using Domain.Entities;
using Infrastructure.Common.Parameters;
using Infrastructure.Common;

namespace Infrastructure.Repositories.Interfaces
{
    public interface ICategoryRepository : IBaseRepository<Category>
    {
        Task<PagedList<Category>> GetCategorList(QueryStringParameters parameters);
    }
}
