using PCS.Entity.Enums;
using System;

namespace PCS.Entity.Dtos.OfferDtos.Response
{
    public class OfferListingDto
    {
        public Guid Id { get; set; }
        public bool ProductSold { get; set; }
        public OfferStatus OfferStatus { get; set; }
        public byte OfferPercentage { get; set; }
        public Guid ProductId { get; set; }
        public string DefaultProductImageUrl { get; set; }
    }
}
