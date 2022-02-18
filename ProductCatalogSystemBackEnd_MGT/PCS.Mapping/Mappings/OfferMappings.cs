using AutoMapper;
using PCS.Entity.Dtos.OfferDtos.Request;
using PCS.Entity.Dtos.OfferDtos.Response;
using PCS.Entity.Enums;
using PCS.Entity.Models;
using System.Linq;

namespace PCS.Mapping.Mappings
{
    public class OfferMappings : Profile
    {
        public OfferMappings()
        {
            CreateMap<OfferCreateDto, Offer>()
                .ForMember(o => o.OfferStatus, m => m.MapFrom(dto => OfferStatus.Waiting));
            CreateMap<Offer, OfferListingDto>()
                .ForMember(dto => dto.DefaultProductImageUrl, m => m.MapFrom(o => o.Product.ProductImages.FirstOrDefault().ImageUrl));
        }
    }
}
