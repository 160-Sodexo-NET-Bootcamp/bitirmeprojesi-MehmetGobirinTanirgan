using AutoMapper;
using PCS.Entity.Dtos.UserDtos.Request;
using PCS.Entity.Dtos.UserDtos.Response;
using PCS.Entity.Models;

namespace PCS.Mapping.Mappings
{
    public class UserMappings : Profile
    {
        public UserMappings()
        {
            CreateMap<UserCreateDto, User>()
                .ForMember(u => u.LockoutEnabled, m => m.MapFrom(dto => false));
            CreateMap<User, UserLoginResponseDto>();
            CreateMap<UserBuyProductDto, Order>();
        }
    }
}
