using AutoMapper;
using ECommerce.Core.Entities;
using ECommerce.Application.DTOs;
using ECommerce.Application.Models;

namespace ECommerce.Application.Mappings
{
    public class ProductMappingProfile : Profile
    {
        public ProductMappingProfile()
        {
            CreateMap<Product, ProductResponseDto>();
            CreateMap<Product, ProductDto>();
            CreateMap<CreateProductDto, Product>();
            CreateMap<UpdateProductDto, Product>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<CreateProductFormModel, CreateProductDto>();
            CreateMap<UpdateProductFormModel, UpdateProductDto>();
            CreateMap<Review, ReviewDto>();
        }
    }
} 