using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using PCS.Core.Filters;
using PCS.Core.Utils.Abstract;
using PCS.Core.Utils.Concrete;
using PCS.Entity.Dtos.CategoryDtos.Request;
using PCS.Entity.Enums;
using PCS.Service.Services.Abstract;
using System;
using System.Threading.Tasks;

namespace PCS.Api.Controllers
{
    [EnableCors("PcsCorsPolicy")]
    [Route("api/Categories")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var serviceResult = await categoryService.GetAllCategoriesAsync();
            return serviceResult.ToObjectResult();
        }

        [Authorize(Roles = Role.Admin), ValidateModel]
        [HttpPost]
        public async Task<IActionResult> AddCategory([FromBody] CategoryCreateDto categoryCreateDto, 
            [FromServices] ICheckableEntityHelper checkableEntityHelper)
        {
            var serviceResult = await categoryService.AddCategoryAsync(categoryCreateDto, checkableEntityHelper);
            return serviceResult.ToObjectResult();
        }

        [Authorize(Roles = Role.Admin), ValidateModel]
        [HttpPut]
        public async Task<IActionResult> UpdateCategory([FromBody] CategoryUpdateDto categoryUpdateDto, 
            [FromServices] ICheckableEntityHelper checkableEntityHelper)
        {
            var serviceResult = await categoryService.UpdateCategoryAsync(categoryUpdateDto, checkableEntityHelper);
            return serviceResult.ToObjectResult();
        }

        [Authorize(Roles = Role.Admin)]
        [HttpDelete("{categoryId:guid}")]
        public async Task<IActionResult> DeleteCategory([FromRoute] Guid categoryId, 
            [FromServices] ICheckableEntityHelper checkableEntityHelper)
        {
            var serviceResult = await categoryService.DeleteCategoryAsync(categoryId, checkableEntityHelper);
            return serviceResult.ToObjectResult();
        }
    }
}
