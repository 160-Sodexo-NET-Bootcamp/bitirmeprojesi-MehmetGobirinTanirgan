using PCS.Core.CoreEntities.Abstract;
using System;

namespace PCS.Entity.Models
{
    public class Order : SoftDeletableEntity<Guid>
    {
        public decimal OrderPrice { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public int PaymentTypeId { get; set; }
        public PaymentType PaymentType { get; set; }
    }
}
