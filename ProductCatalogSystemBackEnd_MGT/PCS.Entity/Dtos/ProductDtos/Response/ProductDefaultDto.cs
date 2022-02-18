using PCS.Entity.Dtos.ProductImageDtos.Response;
using PCS.Entity.Enums;
using System;
using System.Collections.Generic;

namespace PCS.Entity.Dtos.ProductDtos.Response
{
    public class ProductDefaultDto
    {
        public Guid Id { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public ProductUsageCondition UsageCondition { get; set; }
        public bool IsOfferable { get; set; }
        public IEnumerable<ProductImageDefaultDto> ProductImages { get; set; }
    }
}
