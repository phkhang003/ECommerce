using System.ComponentModel.DataAnnotations;

public class CreateProductFormModel
{
    [Required(ErrorMessage = "Name is required")]
    [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Description is required")]
    [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
    public string Description { get; set; } = string.Empty;

    [Required(ErrorMessage = "Price is required")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
    public decimal Price { get; set; }

    [Required(ErrorMessage = "CategoryId is required")]
    public string CategoryId { get; set; } = string.Empty;

    [Required(ErrorMessage = "Stock quantity is required")]
    [Range(0, int.MaxValue, ErrorMessage = "Stock quantity cannot be negative")]
    public int StockQuantity { get; set; }

    [Required(ErrorMessage = "Brand is required")]
    [StringLength(50, ErrorMessage = "Brand cannot exceed 50 characters")]
    public string Brand { get; set; } = string.Empty;
} 