using Application.ErrorHandlers;
using Application.Services.Interfaces;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Common;
using Infrastructure.Common.Parameters;
using Infrastructure.Data;
using Infrastructure.DTOs.Request.Category;
using Infrastructure.DTOs.Request.Product;
using Infrastructure.DTOs.Response.Category;
using Infrastructure.DTOs.Response.Product;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CategoryService> _logger;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork, ILogger<CategoryService> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<ProductResponse> deleteProduct(int id)
        {
            if (id <= 0)
            {
                throw new BadRequestException("Invalid product Id");
            }
            var productEntity = await _unitOfWork.ProductRepository.GetByIdAsync(id);
            if (productEntity == null)
            {
                throw new NotFoundException($"Cannot find product with Id: {id}");
            }

            try
            {
                await _unitOfWork.BeginTransactionAsync();
                await _unitOfWork.ProductRepository.DeleteByAsync(id);
                await _unitOfWork.SaveChangeAsync();

                await _unitOfWork.CommitAsync();
                return _mapper.Map<ProductResponse>(productEntity);
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                _logger.LogError(ex, "Error occurred when trying to delete product");
                throw new Exception("Error occurred when trying to delete product");
            }
        }
        public async Task<ProductResponse> updateProduct(int id, ProductUpdateRequest request)
        {
            if (id <= 0)
            {
                throw new BadRequestException("Invalid product Id");
            }
            var productEntity = await _unitOfWork.ProductRepository.GetByIdAsync(id);
            if (productEntity == null)
            {
                throw new NotFoundException($"Cannot find product with Id: {id}");
            }

            var updatedProduct = _mapper.Map(request, productEntity);
            var categoryEntity = await _unitOfWork.CategoryRepository.GetByIdAsync(updatedProduct.CategoryId);
            if (categoryEntity == null)
            {
                throw new Exception();
            }
            updatedProduct.Category = categoryEntity;
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                _unitOfWork.ProductRepository.UpdateAsync(updatedProduct);
                await _unitOfWork.SaveChangeAsync();

                await _unitOfWork.CommitAsync();
                return _mapper.Map<ProductResponse>(updatedProduct);
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                _logger.LogError(ex, "Error occurred when trying to update product");
                throw new Exception("Error occurred when trying to update product");
            }
        }
        public async Task<ProductResponse> updateProductStatus(int id, ProductStatusUpdateRequest request)
        {
            if (id <= 0)
            {
                throw new BadRequestException("Invalid product Id");
            }
            var productEntity = await _unitOfWork.ProductRepository.GetByIdAsync(id);
            if (productEntity == null)
            {
                throw new NotFoundException($"Cannot find product with Id: {id}");
            }

            var updatedProduct = _mapper.Map(request, productEntity);
            var categoryEntity = await _unitOfWork.CategoryRepository.GetByIdAsync(updatedProduct.CategoryId);
            if (categoryEntity == null)
            {
                throw new Exception();
            }
            updatedProduct.Category = categoryEntity;
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                _unitOfWork.ProductRepository.UpdateAsync(updatedProduct);
                await _unitOfWork.SaveChangeAsync();

                await _unitOfWork.CommitAsync();
                return _mapper.Map<ProductResponse>(updatedProduct);
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                _logger.LogError(ex, "Error occurred when trying to update product");
                throw new Exception("Error occurred when trying to update product");
            }
        }
        public async Task<ProductResponse> updateProductQuantity(int id, ProductQuantityUpdateRequest request)
        {
            if (id <= 0)
            {
                throw new BadRequestException("Invalid product Id");
            }
            var productEntity = await _unitOfWork.ProductRepository.GetByIdAsync(id);
            if (productEntity == null)
            {
                throw new NotFoundException($"Cannot find product with Id: {id}");
            }

            var updatedProduct = _mapper.Map(request, productEntity);
            var categoryEntity = await _unitOfWork.CategoryRepository.GetByIdAsync(updatedProduct.CategoryId);
            if (categoryEntity == null)
            {
                throw new Exception();
            }
            updatedProduct.Category = categoryEntity;
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                _unitOfWork.ProductRepository.UpdateAsync(updatedProduct);
                await _unitOfWork.SaveChangeAsync();

                await _unitOfWork.CommitAsync();
                return _mapper.Map<ProductResponse>(updatedProduct);
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                _logger.LogError(ex, "Error occurred when trying to update product");
                throw new Exception("Error occurred when trying to update product");
            }
        }
        public async Task<ProductResponse> CreateProduct(ProductCreateRequest request)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var categoryEntity = await _unitOfWork.CategoryRepository.GetByIdAsync(request.CategoryId);
                if (categoryEntity == null)
                {
                    throw new Exception();
                }
                var product = _mapper.Map<Product>(request);
                product.Category = categoryEntity;
;               var productEntity = await _unitOfWork.ProductRepository.AddReturnEntityAsync(product);
                await _unitOfWork.SaveChangeAsync();
                if (productEntity == null)
                {
                    throw new Exception();
                }

                await _unitOfWork.CommitAsync();
                return _mapper.Map<ProductResponse>(productEntity);
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                _logger.LogError(ex, "Error occurred when trying to create product");
                throw new Exception("Error occurred when trying to create product");
            }
        }

        public async Task<ProductResponse> GetProductById(int id)
        {
            if (id <= 0)
            {
                throw new BadRequestException("Invalid product Id");
            }
            var productEntity = await _unitOfWork.ProductRepository.GetByIdAsync(id);
            if (productEntity == null)
            {
                throw new NotFoundException($"Cannot find product with Id: {id}");
            }
            return _mapper.Map<ProductResponse>(productEntity);
        }

        public async Task<PagedList<ProductResponse>> GetProductList(ProductListParameters parameters)
        {
            var list = await _unitOfWork.ProductRepository.GetProductList(parameters);
            if (list == null)
            {
                throw new NotFoundException("Cannot find products with given parameters");
            }
            return _mapper.Map<PagedList<ProductResponse>>(list);
        }
    }
}
