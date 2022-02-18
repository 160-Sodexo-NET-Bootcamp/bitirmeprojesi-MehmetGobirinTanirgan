namespace PCS.Core.ResultTypes.Abstract
{
    public interface ISuccessResult : IResult
    {
        string Message { get; set; }
        object Data { get; set; }
    }
}
