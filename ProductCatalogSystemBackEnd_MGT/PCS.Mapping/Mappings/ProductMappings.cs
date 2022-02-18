using AutoMapper;
using PCS.Entity.Dtos.ProductDtos.Request;
using PCS.Entity.Dtos.ProductDtos.Response;
using PCS.Entity.Models;

namespace PCS.Mapping.Mappings
{
    public class ProductMappings : Profile
    {
        public ProductMappings()
        {
            CreateMap<Product, ProductDefaultDto>();
            CreateMap<ProductCreateDto, Product>()
                .ForMember(p => p.IsSold, m => m.MapFrom(dto => false));
            CreateMap<ProductUpdateDto, Product>();
        }
    }
}
