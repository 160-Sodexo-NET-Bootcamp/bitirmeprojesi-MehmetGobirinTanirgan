using PCS.Core.ResultTypes.Abstract;
using PCS.Core.Utils.Abstract;
using PCS.Entity.Dtos.CategoryDtos.Request;
using System;
using System.Threading.Tasks;

namespace PCS.Service.Services.Abstract
{
    public interface ICategoryService
    {
        Task<IResult> GetAllCategoriesAsync();
        Task<IResult> AddCategoryAsync(CategoryCreateDto categoryCreateDto, ICheckableEntityHelper checkableEntityHelper);
        Task<IResult> UpdateCategoryAsync(CategoryUpdateDto categoryUpdateDto, ICheckableEntityHelper checkableEntityHelper);
        Task<IResult> DeleteCategoryAsync(Guid categoryId, ICheckableEntityHelper checkableEntityHelper);
    }
}
