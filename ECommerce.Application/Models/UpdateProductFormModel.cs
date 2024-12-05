using System.ComponentModel.DataAnnotations;

public class UpdateProductFormModel
{
    [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
    public string? Name { get; set; }

    [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
    public string? Description { get; set; }

    [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
    public decimal? Price { get; set; }

    public string? CategoryId { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Stock quantity cannot be negative")]
    public int? StockQuantity { get; set; }

    [StringLength(50, ErrorMessage = "Brand cannot exceed 50 characters")]
    public string? Brand { get; set; }
} 