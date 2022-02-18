using PCS.Core.ResultTypes.Abstract;
using PCS.Entity.Dtos.UserDtos.Request;
using System.Threading.Tasks;

namespace PCS.Service.Services.Abstract
{
    public interface IUserService
    {
        Task<IResult> BuyProductAsync(UserBuyProductDto userBuyProductDto);
    }
}
