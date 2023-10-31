using Infrastructure.Common.Parameters;
using Infrastructure.Common;
using Infrastructure.DTOs.Response.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.DTOs.Response.Product;
using Infrastructure.DTOs.Request.Category;
using Infrastructure.DTOs.Request.Product;

namespace Application.Services.Interfaces
{
    public interface IProductService
    {
        Task<ProductResponse> deleteProduct(int id);
        Task<ProductResponse> updateProduct(int id, ProductUpdateRequest request);
        Task<PagedList<ProductResponse>> GetProductList(QueryStringParameters parameters);
        Task<ProductResponse> GetProductById(int id);
        Task<ProductResponse> CreateProduct(ProductCreateRequest request);
    }
}
