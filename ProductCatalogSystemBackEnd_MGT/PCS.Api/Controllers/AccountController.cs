using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using PCS.Core.Filters;
using PCS.Core.Utils.Abstract;
using PCS.Core.Utils.Concrete;
using PCS.Entity.Dtos.UserDtos.Request;
using PCS.Entity.Enums;
using PCS.Service.Services.Abstract;
using System.Threading.Tasks;

namespace PCS.Api.Controllers
{
    [EnableCors]
    [Route("api/[controller]s")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService accountService;

        public AccountController(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        //Sign up
        [ValidateModel]
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] UserCreateDto userCreateDto)
        {
            var serviceResult = await accountService.RegisterAsync(userCreateDto);
            return serviceResult.ToObjectResult();
        }

        //Sign in
        [ValidateModel]
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto userLoginDto)
        {
            var serviceResult = await accountService.LoginAsync(userLoginDto);
            return serviceResult.ToObjectResult();
        }

        //Refresh token login
        [ValidateModel]
        [HttpPost("RefreshLogin")]
        public async Task<IActionResult> RefreshLogin([FromBody] UserRefreshLoginDto userRefreshLoginDto)
        {
            var serviceResult = await accountService.RefreshLoginAsync(userRefreshLoginDto);
            return serviceResult.ToObjectResult();
        }

        //Password reset
        [Authorize(Roles = Role.Admin + "," + Role.User), ValidateModel]
        [HttpPut("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] UserResetPasswordDto userResetPasswordDto, [FromServices] ICheckableEntityHelper checkableEntityHelper)
        {
            var serviceResult = await accountService.ResetPasswordAsync(userResetPasswordDto, checkableEntityHelper);
            return serviceResult.ToObjectResult();
        }
    }
}
