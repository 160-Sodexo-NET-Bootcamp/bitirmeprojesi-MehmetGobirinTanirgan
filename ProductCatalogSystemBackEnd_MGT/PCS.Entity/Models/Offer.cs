using PCS.Core.CoreEntities.Abstract;
using PCS.Entity.Enums;
using System;

namespace PCS.Entity.Models
{
    public class Offer : SoftDeletableEntity<Guid>
    {
        public OfferStatus OfferStatus { get; set; }
        public byte OfferPercentage { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public Guid ProductOwnerId { get; set; }
        public User ProductOwner { get; set; }
        public Guid OffererId { get; set; }
        public User Offerer { get; set; }
    }
}
