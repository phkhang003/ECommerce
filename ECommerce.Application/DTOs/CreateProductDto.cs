using Microsoft.AspNetCore.Http;

namespace ECommerce.Application.DTOs
{
    public class CreateProductDto
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string CategoryId { get; set; } = string.Empty;
        public int StockQuantity { get; set; }
        public string Brand { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public IFormFile? Image { get; set; }
    }
} 