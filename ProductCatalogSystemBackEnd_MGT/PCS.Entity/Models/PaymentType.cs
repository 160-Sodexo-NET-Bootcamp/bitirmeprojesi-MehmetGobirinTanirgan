using PCS.Core.CoreEntities.Abstract;
using System.Collections.Generic;

namespace PCS.Entity.Models
{
    public class PaymentType : HardDeletableEntity<int>
    {
        public string PaymentMethod { get; set; }
        public IEnumerable<Order> Orders { get; set; }
    }
}
