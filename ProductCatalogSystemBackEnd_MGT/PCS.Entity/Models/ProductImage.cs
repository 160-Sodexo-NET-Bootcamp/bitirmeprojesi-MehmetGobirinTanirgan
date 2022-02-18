using PCS.Core.CoreEntities.Abstract;
using System;

namespace PCS.Entity.Models
{
    public class ProductImage : SoftDeletableEntity<Guid>
    {
        public string ImageUrl { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
    }
}
