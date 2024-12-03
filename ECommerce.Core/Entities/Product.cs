namespace ECommerce.Core.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public string CategoryId { get; set; }
        public List<string> Images { get; set; } = new();
        public bool IsAvailable { get; set; }
        public decimal DiscountPrice { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public List<string> Tags { get; set; } = new();
        public string Brand { get; set; }
        public Dictionary<string, string> Specifications { get; set; } = new();
        public List<Review> Reviews { get; set; } = new();
        public double Rating { get; set; }
    }

    public class Review : BaseEntity
    {
        public string ProductId { get; set; }
        public string UserId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; }
    }
} 