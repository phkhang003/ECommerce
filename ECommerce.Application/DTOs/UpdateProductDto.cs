using Microsoft.AspNetCore.Http;

namespace ECommerce.Application.DTOs
{
    public class UpdateProductDto
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public string? CategoryId { get; set; }
        public int? StockQuantity { get; set; }
        public string? Brand { get; set; }
        public bool? IsActive { get; set; }
        public IFormFile? Image { get; set; }
    }
} 