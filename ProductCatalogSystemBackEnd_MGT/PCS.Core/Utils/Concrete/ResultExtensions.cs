using Microsoft.AspNetCore.Mvc;
using PCS.Core.ResultTypes.Abstract;

namespace PCS.Core.Utils.Concrete
{
    public static class ResultExtensions
    {
        public static ObjectResult ToObjectResult(this IResult result)
        {
            return result.StatusCode switch
            {
                204 => new ObjectResult(null) { StatusCode = result.StatusCode },
                _ => new ObjectResult(result) { StatusCode = result.StatusCode },
            };
        }
    }
}
