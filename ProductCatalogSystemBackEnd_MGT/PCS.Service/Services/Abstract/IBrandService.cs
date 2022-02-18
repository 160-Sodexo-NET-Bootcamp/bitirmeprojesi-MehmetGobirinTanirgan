using PCS.Core.ResultTypes.Abstract;
using PCS.Core.Utils.Abstract;
using PCS.Entity.Dtos.BrandDtos.Request;
using System;
using System.Threading.Tasks;

namespace PCS.Service.Services.Abstract
{
    public interface IBrandService
    {
        Task<IResult> GetAllBrandsAsync();
        Task<IResult> AddBrandAsync(BrandCreateDto brandCreateDto, ICheckableEntityHelper checkableEntityHelper);
        Task<IResult> UpdateBrandAsync(BrandUpdateDto brandUpdateDto, ICheckableEntityHelper checkableEntityHelper);
        Task<IResult> DeleteBrandAsync(Guid brandId, ICheckableEntityHelper checkableEntityHelper);
    }
}
