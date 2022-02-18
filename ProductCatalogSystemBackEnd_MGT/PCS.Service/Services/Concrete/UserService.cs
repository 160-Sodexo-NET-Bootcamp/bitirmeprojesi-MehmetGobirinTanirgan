using AutoMapper;
using PCS.Core.CustomExceptions;
using PCS.Core.ResultTypes.Abstract;
using PCS.Core.Utils.Abstract;
using PCS.Core.Utils.Concrete;
using PCS.Entity.Dtos.UserDtos.Request;
using PCS.Entity.Enums;
using PCS.Entity.Models;
using PCS.Repository.UnitOfWork.Abstract;
using PCS.Service.Services.Abstract;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PCS.Service.Services.Concrete
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly ICheckableEntityHelper checkableEntityHelper;
        private readonly Guid mainUserId;
        public UserService(IUnitOfWork unitOfWork, IMapper mapper, ICheckableEntityHelper checkableEntityHelper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.checkableEntityHelper = checkableEntityHelper;
            mainUserId = checkableEntityHelper.MainUserId;
        }

        public async Task<IResult> BuyProductAsync(UserBuyProductDto userBuyProductDto)
        {
            var existingProduct = await unitOfWork.Products.GetByIdAsync(userBuyProductDto.ProductId, x => x.ProductOffers);

            if (existingProduct is null)
            {
                return ResultGenerator.BadRequest(ErrorMessages.E8);
            }

            if (existingProduct.ProductOwnerId == mainUserId)// User's cannot buy their own products
            {
                throw new ConflictException(ExceptionMessages.U1(mainUserId, existingProduct.Id));
            }

            if (existingProduct.IsSold)
            {
                return ResultGenerator.BadRequest(ErrorMessages.E15);
            }        

            var newOrder = mapper.Map<Order>(userBuyProductDto);
            newOrder.OrderPrice = existingProduct.Price;

            if (existingProduct.IsOfferable)//If product is offerable 
            {
                //Checking if there are accepted offer for this product and calculate discounted price
                var offerListOfProduct = existingProduct.ProductOffers.ToList();
                var acceptedOffer = offerListOfProduct.Where(x => x.OffererId == mainUserId && x.OfferStatus == OfferStatus.Accepted).SingleOrDefault();

                if (acceptedOffer is not null)
                {
                    newOrder.OrderPrice = existingProduct.Price * (decimal)((100 - acceptedOffer.OfferPercentage)/100.0);
                }

                offerListOfProduct.ForEach(x => x.OfferStatus = OfferStatus.ProductSold);
            }

            existingProduct.IsSold = true;
            checkableEntityHelper.AddMainUserAsUpdater(existingProduct);
            unitOfWork.Products.Update(existingProduct);
            checkableEntityHelper.AddMainUserAsCreator(newOrder);
            unitOfWork.Orders.Add(newOrder);
            await unitOfWork.SaveAsync();
            return ResultGenerator.Ok();
        }
    }
}
