using PCS.Core.CoreEntities.Abstract;
using System;
using System.Collections.Generic;

namespace PCS.Entity.Models
{
    public class Brand : SoftDeletableEntity<Guid>
    {
        public string BrandName { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}
