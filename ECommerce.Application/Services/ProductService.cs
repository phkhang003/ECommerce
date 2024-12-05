using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using ECommerce.Core.Entities;
using ECommerce.Core.Interfaces;
using ECommerce.Application.DTOs;
using ECommerce.Application.Models;
using ECommerce.Application.Interfaces;
using AutoMapper;

namespace ECommerce.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Product> _repository;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;
        private readonly ICacheService _cacheService;
        private readonly ILogger<ProductService> _logger;

        public ProductService(
            IRepository<Product> repository,
            IMapper mapper,
            IFileService fileService,
            ICacheService cacheService,
            ILogger<ProductService> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _fileService = fileService;
            _cacheService = cacheService;
            _logger = logger;
        }

        public async Task<PagedResult<ProductResponseDto>> GetProductsAsync(ProductParameters parameters)
        {
            var products = await _repository.GetAllAsync();
            var totalItems = products.Count();
            
            var mappedProducts = _mapper.Map<IEnumerable<ProductResponseDto>>(products);
            
            return new PagedResult<ProductResponseDto>
            {
                Items = mappedProducts,
                TotalItems = totalItems,
                PageNumber = parameters.PageNumber,
                PageSize = parameters.PageSize
            };
        }

        public async Task<ProductResponseDto> GetProductByIdAsync(string id)
        {
            var product = await _repository.GetByIdAsync(id);
            return _mapper.Map<ProductResponseDto>(product);
        }

        public async Task<ProductResponseDto> CreateProductAsync(CreateProductDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);
            await _repository.AddAsync(product);
            return _mapper.Map<ProductResponseDto>(product);
        }

        public async Task<ProductResponseDto> UpdateProductAsync(string id, UpdateProductDto productDto)
        {
            var product = await _repository.GetByIdAsync(id);
            _mapper.Map(productDto, product);
            await _repository.UpdateAsync(product);
            return _mapper.Map<ProductResponseDto>(product);
        }

        public async Task DeleteProductAsync(string id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<ProductDto> AddProductImageAsync(string id, IFormFile image)
        {
            var product = await _repository.GetByIdAsync(id);
            var imageUrl = await _fileService.UploadFileAsync(image);
            product.Images.Add(imageUrl);
            await _repository.UpdateAsync(product);
            return _mapper.Map<ProductDto>(product);
        }

        public async Task<ProductDto> UpdateStockAsync(string id, int quantity)
        {
            var product = await _repository.GetByIdAsync(id);
            product.StockQuantity = quantity;
            await _repository.UpdateAsync(product);
            return _mapper.Map<ProductDto>(product);
        }

        public async Task<ProductDto> AddReviewAsync(string id, CreateReviewDto reviewDto)
        {
            var product = await _repository.GetByIdAsync(id);
            var review = _mapper.Map<Review>(reviewDto);
            product.Reviews.Add(review);
            await _repository.UpdateAsync(product);
            return _mapper.Map<ProductDto>(product);
        }

        public async Task<IEnumerable<ProductDto>> GetProductsByCategoryAsync(string categoryId)
        {
            var products = await _repository.FindAsync(p => p.CategoryId == categoryId);
            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }

        public async Task<IEnumerable<ProductDto>> SearchProductsAsync(string searchTerm)
        {
            var products = await _repository.FindAsync(p => 
                p.Name.Contains(searchTerm) || 
                p.Description.Contains(searchTerm));
            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }

        public async Task<ProductResponseDto> AddTagsAsync(string id, List<string> tags)
        {
            var product = await _repository.GetByIdAsync(id);
            product.Tags.AddRange(tags);
            await _repository.UpdateAsync(product);
            return _mapper.Map<ProductResponseDto>(product);
        }

        public async Task<ProductResponseDto> AddSpecificationsAsync(string id, Dictionary<string, string> specifications)
        {
            var product = await _repository.GetByIdAsync(id);
            foreach (var spec in specifications)
            {
                product.Specifications[spec.Key] = spec.Value;
            }
            await _repository.UpdateAsync(product);
            return _mapper.Map<ProductResponseDto>(product);
        }
    }
} 