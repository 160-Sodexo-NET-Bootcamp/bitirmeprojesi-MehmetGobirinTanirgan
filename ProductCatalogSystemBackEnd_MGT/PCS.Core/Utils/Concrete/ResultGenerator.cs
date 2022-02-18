using PCS.Core.ResultTypes.Concrete;
using System.Collections.Generic;

namespace PCS.Core.Utils.Concrete
{
    public class ResultGenerator
    {
        public static SuccessResult Ok() => new(200);
        public static SuccessResult Ok(string message) => new(200, message);
        public static SuccessResult Ok(string message, object data) => new(200, message, data);
        public static SuccessResult Ok(object data) => new(200, data);
        public static SuccessResult Created() => new(201);
        public static SuccessResult Created(string message) => new(201, message);
        public static SuccessResult Created(string message, object data) => new(201, message, data);
        public static SuccessResult Created(object data) => new(201, data);
        public static SuccessResult NoContent() => new(204);
        public static SuccessResult NoContent(string message) => new(204, message);

        public static ErrorResult BadRequest() => new(400);
        public static ErrorResult BadRequest(string error) => new(400, error);
        public static ErrorResult BadRequest(List<string> errors) => new(400, errors);
        public static ErrorResult Unauthorized() => new(401);
        public static ErrorResult Unauthorized(string error) => new(401, error);
        public static ErrorResult Unauthorized(List<string> errors) => new(401, errors);
        public static ErrorResult Forbidden() => new(403);
        public static ErrorResult Forbidden(string error) => new(403, error);
        public static ErrorResult Forbidden(List<string> errors) => new(403, errors);
        public static ErrorResult NotFound() => new(404);
        public static ErrorResult NotFound(string error) => new(404, error);
        public static ErrorResult NotFound(List<string> errors) => new(404, errors);
    }
}
