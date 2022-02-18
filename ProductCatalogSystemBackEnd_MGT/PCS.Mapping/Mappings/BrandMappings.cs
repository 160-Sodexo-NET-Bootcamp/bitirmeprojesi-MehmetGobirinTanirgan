using AutoMapper;
using PCS.Entity.Dtos.BrandDtos.Request;
using PCS.Entity.Dtos.BrandDtos.Response;
using PCS.Entity.Models;

namespace PCS.Mapping.Mappings
{
    public class BrandMappings : Profile
    {
        public BrandMappings()
        {
            CreateMap<Brand, BrandDefaultDto>();
            CreateMap<BrandCreateDto, Brand>();
            CreateMap<BrandUpdateDto, Brand>();
        }
    }
}
