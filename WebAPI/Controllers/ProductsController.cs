using Application.Services.Implementations;
using Application.Services.Interfaces;
using Domain.Enums;
using Infrastructure.Common.Parameters;
using Infrastructure.Common;
using Infrastructure.DTOs.Response.Category;
using Infrastructure.DTOs.Response;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Infrastructure.DTOs.Response.Product;
using Application.ErrorHandlers;
using Infrastructure.DTOs.Request.Category;
using Infrastructure.DTOs.Request.Product;

namespace WebAPI.Controllers
{
    [Route("api/v1/products")]
    [ApiController]
    public class ProductsController : Controller
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly IProductService _productService;

        public ProductsController(ILogger<ProductsController> logger, IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ResponseObject<PagedList<ProductResponse>>))]
        public async Task<ActionResult> GetCategoryList([FromQuery] QueryStringParameters parameters)
        {
            _logger.LogInformation("Retrieving data of products");
            var result = await _productService.GetProductList(parameters);
            return Ok(new ResponseObject<PagedList<ProductResponse>>()
            {
                Status = ResponseStatus.Success.ToString(),
                Message = "Successfully retrieved paginated list of product",
                Data = result
            });
        }

        [HttpGet]
        [Route("{id}")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ResponseObject<ProductResponse>))]
        [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ErrorDetail))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorDetail))]
        public async Task<ActionResult<ResponseObject<ProductResponse>>> GetProductById([FromRoute] int id)
        {
            _logger.LogInformation("Retrieving data of product with Id: {id}", id);
            var result = await _productService.GetProductById(id);

            return Ok(new ResponseObject<ProductResponse>()
            {
                Status = ResponseStatus.Success.ToString(),
                Message = $"Successfully retrieve data of product with Id: {id}",
                Data = result
            });
        }

        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ResponseObject<ProductResponse>))]
        public async Task<ActionResult<ResponseObject<ProductResponse>>> CreateProduct([FromBody] ProductCreateRequest request)
        {
            _logger.LogInformation("Creating product with name", request.Name);
            var result = await _productService.CreateProduct(request);
            return Ok(new ResponseObject<ProductResponse>()
            {
                Status = ResponseStatus.Success.ToString(),
                Message = $"Successfully create new product with Id: {result.Id}",
                Data = result
            });
        }

        [HttpPut]
        [Route("{id}")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ResponseObject<ProductResponse>))]
        public async Task<ActionResult<ResponseObject<ProductResponse>>> UpdateCategory([FromRoute] int id, [FromBody] ProductUpdateRequest request)
        {
            _logger.LogInformation("Update product with Id: {id}", id);
            var result = await _productService.updateProduct(id, request);
            return Ok(new ResponseObject<ProductResponse>()
            {
                Status = ResponseStatus.Success.ToString(),
                Message = $"Product [Id: {id}] has been updated",
                Data = result
            });
        }

        [HttpDelete]
        [Route("{id}")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ResponseObject<ProductResponse>))]
        public async Task<ActionResult<ResponseObject<ProductResponse>>> DeleteProduct([FromRoute] int id)
        {
            _logger.LogInformation("Delete product with Id: {id}", id);
            var result = await _productService.deleteProduct(id);
            return Ok(new ResponseObject<ProductResponse>()
            {
                Status = ResponseStatus.Success.ToString(),
                Message = $"Product [Id: {id}] has been deleted",
                Data = result
            });
        }
    }
}
