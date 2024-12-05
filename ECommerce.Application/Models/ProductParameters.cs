using System.ComponentModel.DataAnnotations;

namespace ECommerce.Application.Models
{
    public class ProductParameters
    {
        [Required(ErrorMessage = "PageNumber is required")]
        [Range(1, int.MaxValue, ErrorMessage = "PageNumber must be greater than 0")]
        public int PageNumber { get; set; } = 1;

        [Required(ErrorMessage = "PageSize is required")]
        [Range(1, 50, ErrorMessage = "PageSize must be between 1 and 50")]
        public int PageSize { get; set; } = 10;

        public string? SearchTerm { get; set; }

        [RegularExpression("^(name|price|createdAt)$", ErrorMessage = "SortBy must be one of: name, price, createdAt")]
        public string? SortBy { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "MinPrice cannot be negative")]
        public decimal? MinPrice { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "MaxPrice cannot be negative")]
        public decimal? MaxPrice { get; set; }

        public string? CategoryId { get; set; }

        public string? Brand { get; set; }
    }
} 