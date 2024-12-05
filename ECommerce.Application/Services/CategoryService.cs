using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ECommerce.Application.Interfaces;
using ECommerce.Core.Entities;
using ECommerce.Core.Interfaces;
using ECommerce.Application.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;

namespace ECommerce.Application.Services
{
    public class CategoryService : ICategoryApplicationService
    {
        private readonly IRepository<Category> _repository;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;
        private readonly IFileService _fileService;
        private readonly ILogger<CategoryService> _logger;

        public CategoryService(
            IRepository<Category> repository,
            IMapper mapper,
            ICacheService cacheService,
            IFileService fileService,
            ILogger<CategoryService> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _cacheService = cacheService;
            _fileService = fileService;
            _logger = logger;
        }

        public async Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync()
        {
            try
            {
                var cacheKey = "all_categories";
                var categories = await _cacheService.GetAsync<IEnumerable<Category>>(cacheKey);
                
                if (categories == null)
                {
                    categories = await _repository.GetAllAsync();
                    try
                    {
                        await _cacheService.SetAsync(cacheKey, categories);
                    }
                    catch (Exception ex)
                    {
                        // Log cache error but continue
                        _logger.LogError(ex, "Error setting cache for categories");
                    }
                }
                
                return _mapper.Map<IEnumerable<CategoryDto>>(categories);
            }
            catch (Exception ex)
            {
                // If cache fails, fallback to direct database query
                _logger.LogError(ex, "Cache error, falling back to database");
                var categories = await _repository.GetAllAsync();
                return _mapper.Map<IEnumerable<CategoryDto>>(categories);
            }
        }

        public async Task<CategoryDto?> GetCategoryByIdAsync(string id)
        {
            var category = await _repository.GetByIdAsync(id);
            return _mapper.Map<CategoryDto>(category);
        }

        public async Task<CategoryDto> CreateCategoryAsync(CreateCategoryDto createCategoryDto)
        {
            var category = new Category
            {
                Id = ObjectId.GenerateNewId().ToString(),
                Name = createCategoryDto.Name,
                Description = createCategoryDto.Description,
                ParentId = createCategoryDto.ParentId,
                IsActive = createCategoryDto.IsActive
            };

            if (createCategoryDto.Image != null)
            {
                category.ImageUrl = await _fileService.UploadFileAsync(createCategoryDto.Image);
            }

            category.CreatedAt = DateTime.UtcNow;
            category.UpdatedAt = DateTime.UtcNow;

            await _repository.AddAsync(category);
            await _cacheService.RemoveAsync("all_categories");
            
            return _mapper.Map<CategoryDto>(category);
        }

        public async Task<CategoryDto?> UpdateCategoryAsync(string id, UpdateCategoryDto updateCategoryDto)
        {
            var existingCategory = await _repository.GetByIdAsync(id);
            if (existingCategory == null)
                return null;

            if (updateCategoryDto.Image != null)
            {
                if (!string.IsNullOrEmpty(existingCategory.ImageUrl))
                {
                    await _fileService.DeleteFileAsync(existingCategory.ImageUrl);
                }
                existingCategory.ImageUrl = await _fileService.UploadFileAsync(updateCategoryDto.Image);
            }

            _mapper.Map(updateCategoryDto, existingCategory);
            await _repository.UpdateAsync(existingCategory);
            await _cacheService.RemoveAsync("all_categories");
            
            return _mapper.Map<CategoryDto>(existingCategory);
        }

        public async Task<bool> DeleteCategoryAsync(string id)
        {
            var category = await _repository.GetByIdAsync(id);
            if (category == null) return false;

            if (!string.IsNullOrEmpty(category.ImageUrl))
            {
                await _fileService.DeleteFileAsync(category.ImageUrl);
            }

            await _repository.DeleteAsync(id);
            await _cacheService.RemoveAsync("all_categories");
            
            return true;
        }

        public async Task<IEnumerable<CategoryDto>> GetSubcategoriesAsync(string parentId)
        {
            var subcategories = await _repository.FindAsync(c => c.ParentId == parentId);
            return _mapper.Map<IEnumerable<CategoryDto>>(subcategories);
        }
    }
} 