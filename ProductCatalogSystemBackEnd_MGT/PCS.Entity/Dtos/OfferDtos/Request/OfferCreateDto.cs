using System;

namespace PCS.Entity.Dtos.OfferDtos.Request
{
    public class OfferCreateDto
    {
        public Guid ProductId { get; set; }
        public int OfferPercentage { get; set; }
    }
}
