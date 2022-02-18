using PCS.Core.ResultTypes.Abstract;
using PCS.Entity.Dtos.OfferDtos.Request;
using System;
using System.Threading.Tasks;

namespace PCS.Service.Services.Abstract
{
    public interface IOfferService
    {
        Task<IResult> MakeAnOfferAsync(OfferCreateDto offerCreateDto);
        Task<IResult> WithdrawAnOfferAsync(Guid offerId);
        Task<IResult> GetOffersMadeByUserAsync();
        Task<IResult> GetOffersMadeToUserAsync();
        Task<IResult> AcceptOfferAsync(Guid offerId);
        Task<IResult> RejectOfferAsync(Guid offerId);
    }
}
