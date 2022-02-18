using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using PCS.Core.Filters;
using PCS.Core.Utils.Abstract;
using PCS.Core.Utils.Concrete;
using PCS.Entity.Dtos.ProductDtos.Request;
using PCS.Entity.Enums;
using PCS.Service.Services.Abstract;
using System;
using System.Threading.Tasks;

namespace PCS.Api.Controllers
{
    [Authorize(Roles = Role.Admin + "," + Role.User), EnableCors]
    [Route("api/[controller]s")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService productService;

        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }

        [AllowAnonymous]
        [HttpGet("{categoryId:guid}")]
        public async Task<IActionResult> GetProductsOfCategory([FromRoute] Guid categoryId)
        {
            var serviceResult = await productService.GetProductsWithImagesByCategoryIdAsync(categoryId);
            return serviceResult.ToObjectResult();
        }

        [HttpGet("Necessaries")]//Get necessary data for product creation
        public async Task<IActionResult> GetProductCreationNecessaries()
        {
            var serviceResult = await productService.GetNecessaryDataForProductCreationAsync();
            return serviceResult.ToObjectResult();
        }

        [ValidateModel]
        [HttpPost]
        public async Task<IActionResult> AddProduct([FromForm] ProductCreateDto productCreateDto, 
            [FromServices] ICheckableEntityHelper checkableEntityHelper)
        {
            var serviceResult = await productService.AddProductAsync(productCreateDto, checkableEntityHelper);
            return serviceResult.ToObjectResult();
        }

        [ValidateModel]
        [HttpPut]
        public async Task<IActionResult> UpdateProduct([FromBody] ProductUpdateDto productUpdateDto, 
            [FromServices] ICheckableEntityHelper checkableEntityHelper)
        {
            var serviceResult = await productService.UpdateProductAsync(productUpdateDto, checkableEntityHelper);
            return serviceResult.ToObjectResult();
        }

        [HttpDelete("{productId:guid}")]
        public async Task<IActionResult> DeleteProduct([FromRoute] Guid productId, 
            [FromServices] ICheckableEntityHelper checkableEntityHelper)
        {
            var serviceResult = await productService.DeleteProductAsync(productId, checkableEntityHelper);
            return serviceResult.ToObjectResult();
        }
    }
}
