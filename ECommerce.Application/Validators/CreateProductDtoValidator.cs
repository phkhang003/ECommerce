using FluentValidation;
using ECommerce.Application.DTOs;

namespace ECommerce.Application.Validators
{
    public class CreateProductDtoValidator : AbstractValidator<CreateProductDto>
    {
        public CreateProductDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required")
                .MaximumLength(100).WithMessage("Name cannot exceed 100 characters");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required")
                .MaximumLength(500).WithMessage("Description cannot exceed 500 characters");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Price must be greater than 0");

            RuleFor(x => x.CategoryId)
                .NotEmpty().WithMessage("CategoryId is required");

            RuleFor(x => x.StockQuantity)
                .GreaterThanOrEqualTo(0).WithMessage("Stock quantity cannot be negative");

            RuleFor(x => x.Brand)
                .NotEmpty().WithMessage("Brand is required")
                .MaximumLength(50).WithMessage("Brand cannot exceed 50 characters");
        }
    }
} 