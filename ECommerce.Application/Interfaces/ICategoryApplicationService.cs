using ECommerce.Application.DTOs;

namespace ECommerce.Application.Interfaces
{
    public interface ICategoryApplicationService
    {
        Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync();
        Task<CategoryDto?> GetCategoryByIdAsync(string id);
        Task<CategoryDto> CreateCategoryAsync(CreateCategoryDto createCategoryDto);
        Task<CategoryDto?> UpdateCategoryAsync(string id, UpdateCategoryDto updateCategoryDto);
        Task<bool> DeleteCategoryAsync(string id);
        Task<IEnumerable<CategoryDto>> GetSubcategoriesAsync(string parentId);
    }
} 