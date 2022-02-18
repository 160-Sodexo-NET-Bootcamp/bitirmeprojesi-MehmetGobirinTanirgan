using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using PCS.Core.CustomExceptions.Abstract;
using System;
using System.Net;
using System.Threading.Tasks;

namespace PCS.Core.Middlewares
{
    public class GlobalExceptionHandlerMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<GlobalExceptionHandlerMiddleware> logger;

        public GlobalExceptionHandlerMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlerMiddleware> logger)
        {
            this.next = next;
            this.logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                var response = context.Response;
                response.ContentType = "text/plain";

                response.StatusCode = ex switch
                {
                    IClientSideException => (int)HttpStatusCode.BadRequest,
                    _ => (int)HttpStatusCode.InternalServerError
                };

                logger.LogCritical(ex.Message);
                await response.WriteAsync("Oops! Something went wrong!");
            }
        }
    }
}
