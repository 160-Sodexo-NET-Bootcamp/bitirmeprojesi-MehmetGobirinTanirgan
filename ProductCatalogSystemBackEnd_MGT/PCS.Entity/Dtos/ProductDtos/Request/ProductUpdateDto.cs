using System;

namespace PCS.Entity.Dtos.ProductDtos.Request
{
    public class ProductUpdateDto
    {
        public Guid Id { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool IsOfferable { get; set; }
        public Guid CategoryId { get; set; }
        public Guid? BrandId { get; set; }
        public int? ColorId { get; set; }
    }
}
