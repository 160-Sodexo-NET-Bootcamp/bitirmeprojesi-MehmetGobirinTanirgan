using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using PCS.Core.Filters;
using PCS.Core.Utils.Concrete;
using PCS.Entity.Dtos.OfferDtos.Request;
using PCS.Entity.Enums;
using PCS.Service.Services.Abstract;
using System;
using System.Threading.Tasks;

namespace PCS.Api.Controllers
{
    [Authorize(Roles = Role.Admin + "," + Role.User), EnableCors]
    [Route("api/[controller]s")]
    public class OfferController : ControllerBase
    {
        private readonly IOfferService offerService;

        public OfferController(IOfferService offerService)
        {
            this.offerService = offerService;
        }

        [ValidateModel]
        [HttpPost]
        public async Task<IActionResult> MakeAnOffer([FromBody] OfferCreateDto offerCreateDto)
        {
            var serviceResult = await offerService.MakeAnOfferAsync(offerCreateDto);
            return serviceResult.ToObjectResult();
        }

        [HttpDelete("{offerId:guid}")]
        public async Task<IActionResult> WithdrawAnOffer([FromRoute] Guid offerId)
        {
            var serviceResult = await offerService.WithdrawAnOfferAsync(offerId);
            return serviceResult.ToObjectResult();
        }

        [HttpGet("MadeBy")]
        public async Task<IActionResult> GetOffersMadeByUser()
        {
            var serviceResult = await offerService.GetOffersMadeByUserAsync();
            return serviceResult.ToObjectResult();
        }

        [HttpGet("MadeTo")]
        public async Task<IActionResult> GetOffersMadeToUser()
        {
            var serviceResult = await offerService.GetOffersMadeToUserAsync();
            return serviceResult.ToObjectResult();
        }

        [HttpPut("Accept/{offerId:guid}")]
        public async Task<IActionResult> AcceptOffer([FromRoute] Guid offerId)
        {
            var serviceResult = await offerService.AcceptOfferAsync(offerId);
            return serviceResult.ToObjectResult();
        }

        [HttpPut("Reject/{offerId:guid}")]
        public async Task<IActionResult> RejectOffer([FromRoute] Guid offerId)
        {
            var serviceResult = await offerService.RejectOfferAsync(offerId);
            return serviceResult.ToObjectResult();
        }
    }
}
