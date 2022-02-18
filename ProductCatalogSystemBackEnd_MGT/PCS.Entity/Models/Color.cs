using PCS.Core.CoreEntities.Abstract;
using System.Collections.Generic;

namespace PCS.Entity.Models
{
    public class Color : HardDeletableEntity<int>
    {
        public string ColorName { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}
