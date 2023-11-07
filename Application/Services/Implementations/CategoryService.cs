using Application.ErrorHandlers;
using Application.Services.Interfaces;
using AutoMapper;
using Azure.Core;
using Domain.Entities;
using Infrastructure.Common;
using Infrastructure.Common.Parameters;
using Infrastructure.Data;
using Infrastructure.DTOs.Request.Category;
using Infrastructure.DTOs.Response.Category;
using Infrastructure.DTOs.Response.Invoice;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CategoryService> _logger;
        private readonly IMapper _mapper;

        public CategoryService(IUnitOfWork unitOfWork, ILogger<CategoryService> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<CategoryResponse> CreateCategory(CategoryCreateRequest request)
        {
        
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var categoryEntity = await _unitOfWork.CategoryRepository.AddReturnEntityAsync(new Domain.Entities.Category() { Name = request.Name, Status = Domain.Enums.CategoryStatus.Active });
                await _unitOfWork.SaveChangeAsync();
                if (categoryEntity == null)
                {
                    throw new Exception();
                }
 
                await _unitOfWork.CommitAsync();
                return _mapper.Map<CategoryResponse>(categoryEntity);
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                _logger.LogError(ex, "Error occurred when trying to create category");
                throw new Exception("Error occurred when trying to create category");
            }
        }

        public async Task<CategoryResponse> deleteCategory(int id)
        {
            if (id <= 0)
            {
                throw new BadRequestException("Invalid category Id");
            }
            var categoryEntity = await _unitOfWork.CategoryRepository.GetByIdAsync(id);
            if (categoryEntity == null)
            {
                throw new NotFoundException($"Cannot find category with Id: {id}");
            }

            try
            {
                await _unitOfWork.BeginTransactionAsync();
                await _unitOfWork.CategoryRepository.DeleteByAsync(id);
                await _unitOfWork.SaveChangeAsync();

                await _unitOfWork.CommitAsync();
                return _mapper.Map<CategoryResponse>(categoryEntity);
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                _logger.LogError(ex, "Error occurred when trying to delete category");
                throw new Exception("Error occurred when trying to delete category");
            }
        }

        public async Task<CategoryResponse> GetCategoryById(int id)
        {
            if (id <= 0)
            {
                throw new BadRequestException("Invalid category Id");
            }
            var categoryEntity = await _unitOfWork.CategoryRepository.GetByIdAsync(id);
            if (categoryEntity == null)
            {
                throw new NotFoundException($"Cannot find category with Id: {id}");
            }
            return _mapper.Map<CategoryResponse>(categoryEntity);
        }

        public async Task<List<CategoryResponse>> GetCategoryList()
        {
            var query = await _unitOfWork.CategoryRepository.GetAllAsync();
            if (query == null)
            {
                throw new NotFoundException("Cannot find products with given parameters");
            }
            var list = query.ToList();
            
            return _mapper.Map<List<CategoryResponse>>(list);
        }

        public async Task<CategoryResponse> updateCategory(int id, CategoryUpdateRequest request)
        {
            if (id <= 0)
            {
                throw new BadRequestException("Invalid category Id");
            }
            var categoryEntity = await _unitOfWork.CategoryRepository.GetByIdAsync(id);
            if (categoryEntity == null)
            {
                throw new NotFoundException($"Cannot find category with Id: {id}");
            }
            var updatedCategory = _mapper.Map(request, categoryEntity);
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                 _unitOfWork.CategoryRepository.UpdateAsync(updatedCategory);
                await _unitOfWork.SaveChangeAsync();
      
                await _unitOfWork.CommitAsync();
                return _mapper.Map<CategoryResponse>(updatedCategory);
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                _logger.LogError(ex, "Error occurred when trying to create category");
                throw new Exception("Error occurred when trying to create category");
            }
        }
        public async Task<CategoryResponse> updateCategoryStatus(int id, CategoryStatusUpdateRequest request)
        {
            if (id <= 0)
            {
                throw new BadRequestException("Invalid category Id");
            }
            var categoryEntity = await _unitOfWork.CategoryRepository.GetByIdAsync(id);
            if (categoryEntity == null)
            {
                throw new NotFoundException($"Cannot find category with Id: {id}");
            }
            var updatedCategory = _mapper.Map(request, categoryEntity);
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                _unitOfWork.CategoryRepository.UpdateAsync(updatedCategory);
                await _unitOfWork.SaveChangeAsync();

                await _unitOfWork.CommitAsync();
                return _mapper.Map<CategoryResponse>(updatedCategory);
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                _logger.LogError(ex, "Error occurred when trying to create category");
                throw new Exception("Error occurred when trying to create category");
            }
        }
    }
}
