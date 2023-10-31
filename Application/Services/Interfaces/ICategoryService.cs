using Infrastructure.Common.Parameters;
using Infrastructure.Common;
using Infrastructure.DTOs.Request.Category;
using Infrastructure.DTOs.Request.Invoice;
using Infrastructure.DTOs.Response.Invoice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.DTOs.Response.Category;

namespace Application.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<CategoryResponse> deleteCategory(int id);
        Task<CategoryResponse> updateCategory(int id,CategoryUpdateRequest request);
        Task<CategoryResponse> GetCategoryById(int id);
        Task<PagedList<CategoryResponse>> GetCategoryList(QueryStringParameters parameters);
        Task<CategoryResponse> CreateCategory(CategoryCreateRequest request);
    }
}
