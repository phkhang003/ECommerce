using AutoMapper;
using ECommerce.Application.Models;
using ECommerce.Application.DTOs;
using ECommerce.Core.Entities;

namespace ECommerce.Application.Mappings
{
    public class CategoryMappingProfile : Profile
    {
        public CategoryMappingProfile()
        {
            CreateMap<CreateCategoryFormModel, CreateCategoryDto>();
            CreateMap<UpdateCategoryFormModel, UpdateCategoryDto>();
            CreateMap<Category, CategoryDto>();
            CreateMap<CreateCategoryDto, Category>();
            CreateMap<UpdateCategoryDto, Category>();
        }
    }
} 