using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using ECommerce.Application.Attributes;

public class CreateCategoryFormModel
{
    [Required(ErrorMessage = "Name is required")]
    [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Description is required")]
    [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
    public string Description { get; set; } = string.Empty;

    public string? ParentId { get; set; }

    public bool IsActive { get; set; } = true;

    [DataType(DataType.Upload)]
    public IFormFile? Image { get; set; }
} 