using AutoMapper;
using PCS.Entity.Dtos.ProductImageDtos.Response;
using PCS.Entity.Models;

namespace PCS.Mapping.Mappings
{
    public class ProductImageMappings : Profile
    {
        public ProductImageMappings()
        {
            CreateMap<ProductImage, ProductImageDefaultDto>();
        }
    }
}
