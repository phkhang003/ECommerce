using Microsoft.AspNetCore.Http;
using ECommerce.Application.DTOs;
using ECommerce.Application.Models;

namespace ECommerce.Application.Interfaces
{
    public interface IProductService
    {
        Task<PagedResult<ProductResponseDto>> GetProductsAsync(ProductParameters parameters);
        Task<ProductResponseDto> GetProductByIdAsync(string id);
        Task<ProductResponseDto> CreateProductAsync(CreateProductDto productDto);
        Task<ProductResponseDto> UpdateProductAsync(string id, UpdateProductDto productDto);
        Task DeleteProductAsync(string id);
        Task<ProductDto> AddProductImageAsync(string id, IFormFile image);
        Task<ProductDto> UpdateStockAsync(string id, int quantity);
        Task<ProductDto> AddReviewAsync(string id, CreateReviewDto reviewDto);
        Task<IEnumerable<ProductDto>> GetProductsByCategoryAsync(string categoryId);
        Task<IEnumerable<ProductDto>> SearchProductsAsync(string searchTerm);
    }
} 