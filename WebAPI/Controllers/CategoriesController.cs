using Application.ErrorHandlers;
using Application.Services.Implementations;
using Application.Services.Interfaces;
using Domain.Enums;
using Infrastructure.DTOs.Response.Invoice;
using Infrastructure.DTOs.Response;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Infrastructure.DTOs.Request.Invoice;
using Microsoft.AspNetCore.Authorization;
using Infrastructure.DTOs.Request.Category;
using Infrastructure.Common.Parameters;
using Infrastructure.Common;
using WebAPI.OptionsSetup;
using Infrastructure.DTOs.Response.Category;

namespace WebAPI.Controllers
{
    [Route("api/v1/categories")]
    [ApiController]
    public class CategoriesController : Controller
    {
        private readonly ILogger<CategoriesController> _logger;
        private readonly ICategoryService _categoryService;

        public CategoriesController(ILogger<CategoriesController> logger, ICategoryService categoryService)
        {
            _logger = logger;
            _categoryService = categoryService;
        }

        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ResponseObject<CategoryResponse>))]
        public async Task<ActionResult<ResponseObject<CategoryResponse>>> CreateInvoice([FromBody] CategoryCreateRequest request)
        {
            _logger.LogInformation("Creating category with name", request.Name);
            var result = await _categoryService.CreateCategory(request);
            return Ok(new ResponseObject<CategoryResponse>()
            {
                Status = ResponseStatus.Success.ToString(),
                Message = $"Successfully create new category with Id: {result.Id}",
                Data = result
            });
        }

        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ResponseObject<List<CategoryResponse>>))]
        public async Task<ActionResult<ResponseObject<List<CategoryResponse>>>> GetCategoryList()
        {
            _logger.LogInformation("Retrieving data of categories");
            var result = await _categoryService.GetCategoryList();
            return Ok(new ResponseObject<List<CategoryResponse>>()
            {
                Status = ResponseStatus.Success.ToString(),
                Message = "Successfully retrieved paginated list of category",
                Data = result
            });
        }
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ResponseObject<CategoryResponse>))]
        [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ErrorDetail))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorDetail))]
        public async Task<ActionResult<ResponseObject<CategoryResponse>>> GetCategoryById([FromRoute] int id)
        {
            _logger.LogInformation("Retrieving data of category with Id: {id}", id);
            var result = await _categoryService.GetCategoryById(id);

            return Ok(new ResponseObject<CategoryResponse>()
            {
                Status = ResponseStatus.Success.ToString(),
                Message = $"Successfully retrieve data of category with Id: {id}",
                Data = result
            });
        }

        [HttpPut("{id}")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ResponseObject<CategoryResponse>))]
        public async Task<ActionResult<ResponseObject<CategoryResponse>>> UpdateCategory([FromRoute] int id,[FromBody] CategoryUpdateRequest request)
        {
            _logger.LogInformation("Update category with Id: {id}", id);
            var result = await _categoryService.updateCategory(id,request);
            return Ok(new ResponseObject<CategoryResponse>()
            {
                Status = ResponseStatus.Success.ToString(),
                Message = $"Category [Id: {id}] has been updated",
                Data = result
            });
        }

        [HttpPut("/{id}/cstatus")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ResponseObject<CategoryResponse>))]
        public async Task<ActionResult<ResponseObject<CategoryResponse>>> UpdateCategoryStatus([FromRoute] int id, [FromBody] CategoryStatusUpdateRequest request)
        {
            _logger.LogInformation("Update category with Id: {id}", id);
            var result = await _categoryService.updateCategoryStatus(id, request);
            return Ok(new ResponseObject<CategoryResponse>()
            {
                Status = ResponseStatus.Success.ToString(),
                Message = $"Category [Id: {id}] has been updated",
                Data = result
            });
        }

        [HttpDelete("{id}")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ResponseObject<CategoryResponse>))]
        public async Task<ActionResult<ResponseObject<CategoryResponse>>> DeleteCategory([FromRoute] int id)
        {
            _logger.LogInformation("Delete category with Id: {id}", id);
            var result = await _categoryService.deleteCategory(id);
            return Ok(new ResponseObject<CategoryResponse>()
            {
                Status = ResponseStatus.Success.ToString(),
                Message = $"Category [Id: {id}] has been deleted",
                Data = result
            });
        }
    }
}
