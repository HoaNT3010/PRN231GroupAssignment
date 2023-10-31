using Domain.Entities;
using Infrastructure.Common.Parameters;
using Infrastructure.Common;

namespace Infrastructure.Repositories.Interfaces
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        Task<PagedList<Product>> GetProductList(QueryStringParameters parameters);
    }
}
