using Microsoft.AspNetCore.Http;
using PCS.Entity.Enums;
using System;
using System.Collections.Generic;

namespace PCS.Entity.Dtos.ProductDtos.Request
{
    public class ProductCreateDto
    {
        public string ProductName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool IsOfferable { get; set; }
        public ProductUsageCondition UsageCondition { get; set; }
        public Guid CategoryId { get; set; }
        public Guid? BrandId { get; set; }
        public int? ColorId { get; set; }
        public List<IFormFile> ImageFiles { get; set; }
    }
}
