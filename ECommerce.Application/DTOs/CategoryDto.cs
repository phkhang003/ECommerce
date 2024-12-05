using Microsoft.AspNetCore.Http;

namespace ECommerce.Application.DTOs
{
    public class CategoryDto
    {
        public string? Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? ParentId { get; set; }
        public string Slug { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public string? ImageUrl { get; set; }
    }

    public class CreateCategoryDto
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? ParentId { get; set; }
        public bool IsActive { get; set; } = true;
        public IFormFile? Image { get; set; }
    }

    public class UpdateCategoryDto
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? ParentId { get; set; }
        public bool IsActive { get; set; } = true;
        public IFormFile? Image { get; set; }
    }
} 