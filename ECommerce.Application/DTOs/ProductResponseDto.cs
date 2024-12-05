namespace ECommerce.Application.DTOs
{
    public class ProductResponseDto
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string CategoryId { get; set; } = string.Empty;
        public List<string> Images { get; set; } = new();
        public int StockQuantity { get; set; }
        public bool IsAvailable { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string Brand { get; set; } = string.Empty;
    }
} 