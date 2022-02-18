using PCS.Core.ResultTypes.Abstract;
using System.Runtime.Serialization;

namespace PCS.Core.ResultTypes.Concrete
{
    public class Result : IResult
    {
        public Result(int statusCode)
        {
            StatusCode = statusCode;
        }

        [IgnoreDataMember]
        public int StatusCode { get; set; }
    }
}
