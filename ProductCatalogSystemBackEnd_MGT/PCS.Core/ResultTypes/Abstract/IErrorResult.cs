using System.Collections.Generic;

namespace PCS.Core.ResultTypes.Abstract
{
    public interface IErrorResult : IResult
    {
        List<string> Errors { get; set; }
    }
}
