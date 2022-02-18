using PCS.Entity.Dtos.BrandDtos.Response;
using PCS.Entity.Dtos.CategoryDtos.Response;
using PCS.Entity.Dtos.ColorDtos.Response;
using System.Collections.Generic;

namespace PCS.Entity.Dtos.ProductDtos.Response
{
    public class ProductCreationNecessariesDto
    {
        public IEnumerable<CategoryDefaultDto> Categories { get; set; }
        public IEnumerable<BrandDefaultDto> Brands { get; set; }
        public IEnumerable<ColorDefaultDto> Colors { get; set; }
    }
}
