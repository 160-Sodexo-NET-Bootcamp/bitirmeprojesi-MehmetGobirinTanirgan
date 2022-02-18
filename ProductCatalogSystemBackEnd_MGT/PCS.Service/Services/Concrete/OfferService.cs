using AutoMapper;
using PCS.Core.CustomExceptions;
using PCS.Core.ResultTypes.Abstract;
using PCS.Core.Utils.Abstract;
using PCS.Core.Utils.Concrete;
using PCS.Entity.Dtos.OfferDtos.Request;
using PCS.Entity.Dtos.OfferDtos.Response;
using PCS.Entity.Enums;
using PCS.Entity.Models;
using PCS.Repository.UnitOfWork.Abstract;
using PCS.Service.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PCS.Service.Services.Concrete
{
    public class OfferService : IOfferService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly ICheckableEntityHelper checkableEntityHelper;
        private readonly Guid mainUserId;
        public OfferService(IUnitOfWork unitOfWork, IMapper mapper, ICheckableEntityHelper checkableEntityHelper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.checkableEntityHelper = checkableEntityHelper;
            mainUserId = checkableEntityHelper.MainUserId;
        }

        public async Task<IResult> MakeAnOfferAsync(OfferCreateDto offerCreateDto)
        {
            //Getting product
            var existingProduct = await unitOfWork.Products.GetByIdAsync(offerCreateDto.ProductId, x => x.ProductOffers);

            if (existingProduct is null)//Checking product
            {
                return ResultGenerator.NotFound(ErrorMessages.E8);
            }

            if (existingProduct.IsSold)//Is it sold
            {
                throw new ConflictException(ExceptionMessages.O8(mainUserId, offerCreateDto.ProductId));
            }

            if (existingProduct.ProductOwnerId == mainUserId)//Product owner and offerer can not be same
            {
                throw new ConflictException(ExceptionMessages.O1(mainUserId, offerCreateDto.ProductId));
            }

            if (!existingProduct.IsOfferable)// Is product offerable
            {
                throw new ConflictException(ExceptionMessages.O2(mainUserId, offerCreateDto.ProductId));
            }

            //Does the user already have an offer for this product
            var checkOffer = existingProduct.ProductOffers.FirstOrDefault(x => x.OffererId == mainUserId && x.OfferStatus == OfferStatus.Waiting);
            if (checkOffer is not null)
            {
                throw new ConflictException(ExceptionMessages.O3(mainUserId,offerCreateDto.ProductId));
            }

            //If there are no problems then create a new offer
            var newOffer = mapper.Map<Offer>(offerCreateDto);
            newOffer.ProductOwnerId = existingProduct.ProductOwnerId;
            newOffer.OffererId = mainUserId;
            checkableEntityHelper.AddMainUserAsCreator(newOffer);
            unitOfWork.Offers.Add(newOffer);
            await unitOfWork.SaveAsync();
            return ResultGenerator.Created();
        }

        public async Task<IResult> WithdrawAnOfferAsync(Guid offerId)
        {
            var existingOffer = await unitOfWork.Offers.GetByIdAsync(offerId);
            if (existingOffer is null)//Checking
            {
                return ResultGenerator.NotFound(ErrorMessages.E8);
            }

            
            if (existingOffer.OffererId != mainUserId)// Offer owner and request owner can not be different
            {
                throw new ConflictException(ExceptionMessages.O4(mainUserId, offerId));
            }

          
            if (existingOffer.OfferStatus == OfferStatus.ProductSold) //Offer status checking
            {
                return ResultGenerator.BadRequest(ErrorMessages.E11);
            }

            
            if (existingOffer.OfferStatus == OfferStatus.Accepted || existingOffer.OfferStatus == OfferStatus.Rejected ||
                existingOffer.OfferStatus == OfferStatus.Withdrew)//Offer status checking
            {
                throw new ConflictException(ExceptionMessages.O5(mainUserId, offerId));
            }

            checkableEntityHelper.AddMainUserAsUpdater(existingOffer);
            unitOfWork.Offers.Delete(existingOffer);
            await unitOfWork.SaveAsync();
            return ResultGenerator.Ok();
        }

        public async Task<IResult> GetOffersMadeByUserAsync()
        {
            var givenOffers = await unitOfWork.Offers.GetOffersMadeByUserAsync(mainUserId);

            if (!givenOffers.Any())
            {
                return ResultGenerator.NoContent(ErrorMessages.E10);
            }
            var givenOfferListingDtos = mapper.Map<IEnumerable<OfferListingDto>>(givenOffers);
            return ResultGenerator.Ok(givenOfferListingDtos);
        }

        public async Task<IResult> GetOffersMadeToUserAsync()
        {
            var takenOffers = await unitOfWork.Offers.GetOffersMadeToUserAsync(mainUserId);

            if (!takenOffers.Any())
            {
                return ResultGenerator.NoContent(ErrorMessages.E10);
            }

            var takenOfferListingDtos = mapper.Map<IEnumerable<OfferListingDto>>(takenOffers);
            return ResultGenerator.Ok(takenOfferListingDtos);
        }

        public async Task<IResult> AcceptOfferAsync(Guid offerId)
        {
            var existingOffer = await unitOfWork.Offers.GetByIdAsync(offerId);

            if (existingOffer is null)
            {
                return ResultGenerator.NotFound(ErrorMessages.E8);
            }

            if (existingOffer.ProductOwnerId != mainUserId)//Offer owner and request owner can not be different
            {
                throw new ConflictException(ExceptionMessages.O6(mainUserId, offerId));
            }

            if (existingOffer.OfferStatus != OfferStatus.Waiting)//Offer status checking
            {
                throw new ConflictException(ExceptionMessages.O5(mainUserId, offerId));
            }

            existingOffer.OfferStatus = OfferStatus.Accepted;
            checkableEntityHelper.AddMainUserAsUpdater(existingOffer);
            unitOfWork.Offers.Update(existingOffer);
            await unitOfWork.SaveAsync();
            return ResultGenerator.Ok();
        }

        public async Task<IResult> RejectOfferAsync(Guid offerId)
        {
            var existingOffer = await unitOfWork.Offers.GetByIdAsync(offerId);

            if (existingOffer is null)
            {
                return ResultGenerator.NotFound(ErrorMessages.E8);
            }

            if (existingOffer.ProductOwnerId != mainUserId)//Offer owner and request owner can not be different
            {
                throw new ConflictException(ExceptionMessages.O6(mainUserId, offerId));
            }

            if (existingOffer.OfferStatus != OfferStatus.Waiting)//Offer status checking
            {
                throw new ConflictException(ExceptionMessages.O5(mainUserId, offerId));
            }

            existingOffer.OfferStatus = OfferStatus.Rejected;
            checkableEntityHelper.AddMainUserAsUpdater(existingOffer);
            unitOfWork.Offers.Update(existingOffer);
            await unitOfWork.SaveAsync();
            return ResultGenerator.Ok();
        }
    }
}
