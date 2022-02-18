using AutoMapper;
using PCS.Entity.Dtos.CategoryDtos.Request;
using PCS.Entity.Dtos.CategoryDtos.Response;
using PCS.Entity.Models;

namespace PCS.Mapping.Mappings
{
    public class CategoryMappings : Profile
    {
        public CategoryMappings()
        {
            CreateMap<Category, CategoryDefaultDto>();
            CreateMap<CategoryCreateDto, Category>();
            CreateMap<CategoryUpdateDto, Category>();
        }
    }
}
