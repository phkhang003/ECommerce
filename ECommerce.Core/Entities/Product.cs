namespace ECommerce.Core.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string CategoryId { get; set; } = string.Empty;
        public List<string> Images { get; set; } = new();
        public int StockQuantity { get; set; }
        public decimal DiscountPrice { get; set; }
        public List<string> Tags { get; set; } = new();
        public string Brand { get; set; } = string.Empty;
        public Dictionary<string, string> Specifications { get; set; } = new();
        public List<Review> Reviews { get; set; } = new();
        public double Rating { get; set; }
    }

    public class Review : BaseEntity
    {
        public string ProductId { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public int Rating { get; set; }
        public string Comment { get; set; } = string.Empty;
    }
} 