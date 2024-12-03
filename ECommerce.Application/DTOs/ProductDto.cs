namespace ECommerce.Application.DTOs
{
    public class ProductDto
    {
        public string? Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string CategoryId { get; set; } = string.Empty;
        public List<string> Images { get; set; } = new();
        public int StockQuantity { get; set; }
        public bool IsAvailable { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<string> Tags { get; set; } = new();
        public string Brand { get; set; } = string.Empty;
        public Dictionary<string, string> Specifications { get; set; } = new();
        public List<ReviewDto> Reviews { get; set; } = new();
    }
} 