using PCS.Core.ResultTypes.Abstract;
using PCS.Core.Utils.Abstract;
using PCS.Entity.Dtos.UserDtos.Request;
using System.Threading.Tasks;

namespace PCS.Service.Services.Abstract
{
    public interface IAccountService
    {
        Task<IResult> RegisterAsync(UserCreateDto userCreateDto);
        Task<IResult> LoginAsync(UserLoginDto userLoginDto);
        Task<IResult> RefreshLoginAsync(UserRefreshLoginDto userRefreshLoginDto);
        Task<IResult> ResetPasswordAsync(UserResetPasswordDto userResetPasswordDto, ICheckableEntityHelper checkableEntityHelper);
    }
}
