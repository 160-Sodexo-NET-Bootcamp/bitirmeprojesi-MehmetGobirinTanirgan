using PCS.Core.ResultTypes.Abstract;
using System.Collections.Generic;

namespace PCS.Core.ResultTypes.Concrete
{
    public class ErrorResult : Result, IErrorResult
    {
        public ErrorResult(int statusCode) : base(statusCode)
        {

        }

        public ErrorResult(int statusCode, string error) : base(statusCode)
        {
            Errors = new List<string> { error };
        }

        public ErrorResult(int statusCode, List<string> errors) : base(statusCode)
        {
            Errors = errors;
        }

        public List<string> Errors { get; set; }
    }
}
