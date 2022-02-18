using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using PCS.Core.Filters;
using PCS.Core.Utils.Abstract;
using PCS.Core.Utils.Concrete;
using PCS.Entity.Dtos.BrandDtos.Request;
using PCS.Entity.Enums;
using PCS.Service.Services.Abstract;
using System;
using System.Threading.Tasks;

namespace PCS.Api.Controllers
{
    [EnableCors]
    [Route("api/[controller]s")]
    public class BrandController : ControllerBase
    {
        private readonly IBrandService brandService;

        public BrandController(IBrandService brandService)
        {
            this.brandService = brandService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var serviceResult = await brandService.GetAllBrandsAsync();
            return serviceResult.ToObjectResult();
        }

        [Authorize(Roles = Role.Admin), ValidateModel]
        [HttpPost]
        public async Task<IActionResult> AddBrand([FromBody] BrandCreateDto brandCreateDto, 
            [FromServices] ICheckableEntityHelper checkableEntityHelper)
        {
            var serviceResult = await brandService.AddBrandAsync(brandCreateDto, checkableEntityHelper);
            return serviceResult.ToObjectResult();
        }

        [Authorize(Roles = Role.Admin), ValidateModel]
        [HttpPut]
        public async Task<IActionResult> UpdateBrand([FromBody] BrandUpdateDto brandUpdateDto, 
            [FromServices] ICheckableEntityHelper checkableEntityHelper)
        {
            var serviceResult = await brandService.UpdateBrandAsync(brandUpdateDto, checkableEntityHelper);
            return serviceResult.ToObjectResult();
        }

        [Authorize(Roles = Role.Admin)]
        [HttpDelete("{brandId:guid}")]
        public async Task<IActionResult> DeleteBrand([FromRoute] Guid brandId, 
            [FromServices] ICheckableEntityHelper checkableEntityHelper)
        {
            var serviceResult = await brandService.DeleteBrandAsync(brandId, checkableEntityHelper);
            return serviceResult.ToObjectResult();
        }
    }
}
