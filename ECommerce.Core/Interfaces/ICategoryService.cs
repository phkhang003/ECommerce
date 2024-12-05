using ECommerce.Core.Entities;

namespace ECommerce.Core.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetAllCategoriesAsync();
        Task<Category?> GetCategoryByIdAsync(string id);
        Task<Category> CreateCategoryAsync(Category category);
        Task<Category?> UpdateCategoryAsync(string id, Category category);
        Task<bool> DeleteCategoryAsync(string id);
        Task<IEnumerable<Category>> GetSubcategoriesAsync(string parentId);
    }
} 