using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using ECommerce.Application.Attributes;

public class UpdateCategoryFormModel
{
    [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
    public string? Name { get; set; }

    [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
    public string? Description { get; set; }

    public string? ParentId { get; set; }

    public bool? IsActive { get; set; }

    [DataType(DataType.Upload)]
    public IFormFile? Image { get; set; }
} 