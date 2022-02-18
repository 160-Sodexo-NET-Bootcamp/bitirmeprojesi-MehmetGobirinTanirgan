using PCS.Core.ResultTypes.Abstract;
using PCS.Core.Utils.Abstract;
using PCS.Entity.Dtos.ColorDtos.Request;
using System.Threading.Tasks;

namespace PCS.Service.Services.Abstract
{
    public interface IColorService
    {
        Task<IResult> GetAllColorsAsync();
        Task<IResult> AddColorAsync(ColorCreateDto colorCreateDto, ICheckableEntityHelper checkableEntityHelper);
        Task<IResult> UpdateColorAsync(ColorUpdateDto colorUpdateDto, ICheckableEntityHelper checkableEntityHelper);
        Task<IResult> DeleteColorAsync(int colorId);
    }
}
