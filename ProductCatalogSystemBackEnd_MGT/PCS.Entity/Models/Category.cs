using PCS.Core.CoreEntities.Abstract;
using System;
using System.Collections.Generic;

namespace PCS.Entity.Models
{
    public class Category : SoftDeletableEntity<Guid>
    {
        public string CategoryName { get; set; }
        public int CategoryLevel { get; set; }
        public int LeftBorder { get; set; }
        public int RightBorder { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}
