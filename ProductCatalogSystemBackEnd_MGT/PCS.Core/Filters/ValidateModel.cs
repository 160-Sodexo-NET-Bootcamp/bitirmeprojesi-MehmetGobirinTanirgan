using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PCS.Core.ResultTypes.Concrete;
using System.Linq;

namespace PCS.Core.Filters
{
    public class ValidateModel : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errorResult = new ErrorResult(400, context.ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList());
                context.Result = new ObjectResult(errorResult) { StatusCode = errorResult.StatusCode };
            }
        }
    }
}
