using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using PCS.Core.Filters;
using PCS.Core.Utils.Concrete;
using PCS.Entity.Dtos.UserDtos.Request;
using PCS.Entity.Enums;
using PCS.Service.Services.Abstract;
using System.Threading.Tasks;

namespace PCS.Api.Controllers
{
    [Authorize(Roles = Role.Admin + "," + Role.User), EnableCors]
    [Route("api/[controller]s")]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [ValidateModel]
        [HttpPost("BuyProduct")]
        public async Task<IActionResult> BuyProduct([FromBody] UserBuyProductDto userBuyProductDto)
        {
            var serviceResult = await userService.BuyProductAsync(userBuyProductDto);
            return serviceResult.ToObjectResult();
        }
    }
}
