using PCS.Core.ResultTypes.Abstract;

namespace PCS.Core.ResultTypes.Concrete
{
    public class SuccessResult : Result, ISuccessResult
    {
        public SuccessResult(int statusCode) : base(statusCode)
        {
            
        }

        public SuccessResult(int statusCode,string message) : base(statusCode)
        {
            Message = message;
        }

        public SuccessResult(int statusCode, string message, object data) : base(statusCode)
        {
            Message = message;
            Data = data;
        }

        public SuccessResult(int statusCode, object data) : base(statusCode)
        {
            Data = data;
        }

        public object Data { get; set; }
        public string Message { get; set; }
    }
}
