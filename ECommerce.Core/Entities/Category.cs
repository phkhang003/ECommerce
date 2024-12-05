using ECommerce.Core.Common;

namespace ECommerce.Core.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? ParentId { get; set; }
        public string Slug { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public string? ImageUrl { get; set; }
        public new DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public new DateTime? UpdatedAt { get; set; }
    }
} 