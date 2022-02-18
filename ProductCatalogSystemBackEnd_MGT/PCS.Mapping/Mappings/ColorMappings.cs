using AutoMapper;
using PCS.Entity.Dtos.ColorDtos.Request;
using PCS.Entity.Dtos.ColorDtos.Response;
using PCS.Entity.Models;

namespace PCS.Mapping.Mappings
{
    public class ColorMappings : Profile
    {
        public ColorMappings()
        {
            CreateMap<Color, ColorDefaultDto>();
            CreateMap<ColorCreateDto, Color>();
            CreateMap<ColorUpdateDto, Color>();
        }
    }
}
