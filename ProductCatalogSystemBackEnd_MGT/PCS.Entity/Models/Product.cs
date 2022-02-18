using PCS.Core.CoreEntities.Abstract;
using PCS.Entity.Enums;
using System;
using System.Collections.Generic;

namespace PCS.Entity.Models
{
    public class Product : SoftDeletableEntity<Guid>
    {
        public string ProductName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool IsOfferable { get; set; }
        public bool IsSold { get; set; } = false;
        public ProductUsageCondition UsageCondition { get; set; }
        public Guid ProductOwnerId { get; set; }
        public User ProductOwner { get; set; }
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
        public Guid? BrandId { get; set; }
        public Brand Brand { get; set; }
        public int? ColorId { get; set; }
        public Color Color { get; set; }
        public IEnumerable<ProductImage> ProductImages { get; set; }
        public IEnumerable<Offer> ProductOffers { get; set; }
        public IEnumerable<Order> Orders { get; set; }

    }
}
