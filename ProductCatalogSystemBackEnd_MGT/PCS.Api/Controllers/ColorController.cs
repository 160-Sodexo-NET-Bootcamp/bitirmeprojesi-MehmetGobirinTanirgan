using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using PCS.Core.Filters;
using PCS.Core.Utils.Abstract;
using PCS.Core.Utils.Concrete;
using PCS.Entity.Dtos.ColorDtos.Request;
using PCS.Entity.Enums;
using PCS.Service.Services.Abstract;
using System.Threading.Tasks;

namespace PCS.Api.Controllers
{
    [EnableCors]
    [Route("api/[controller]s")]
    public class ColorController : ControllerBase
    {
        private readonly IColorService colorService;

        public ColorController(IColorService colorService)
        {
            this.colorService = colorService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var serviceResult = await colorService.GetAllColorsAsync();
            return serviceResult.ToObjectResult();
        }

        [Authorize(Roles = Role.Admin), ValidateModel]
        [HttpPost]
        public async Task<IActionResult> AddColor([FromBody] ColorCreateDto colorCreateDto, 
            [FromServices] ICheckableEntityHelper checkableEntityHelper)
        {
            var serviceResult = await colorService.AddColorAsync(colorCreateDto, checkableEntityHelper);
            return serviceResult.ToObjectResult();
        }

        [Authorize(Roles = Role.Admin), ValidateModel]
        [HttpPut]
        public async Task<IActionResult> UpdateColor([FromBody] ColorUpdateDto colorUpdateDto, 
            [FromServices] ICheckableEntityHelper checkableEntityHelper)
        {
            var serviceResult = await colorService.UpdateColorAsync(colorUpdateDto, checkableEntityHelper);
            return serviceResult.ToObjectResult();
        }

        [Authorize(Roles = Role.Admin)]
        [HttpDelete("{colorId:int:min(1)}")]
        public async Task<IActionResult> DeleteColor([FromRoute] int colorId)
        {
            var serviceResult = await colorService.DeleteColorAsync(colorId);
            return serviceResult.ToObjectResult();
        }
    }
}
