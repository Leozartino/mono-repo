using Kuba.Api.Errors;
using System.Net;
using System.Text.Json;

namespace Kuba.Api.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _environment;

        public ExceptionMiddleware
            (
            RequestDelegate next, 
            ILogger<ExceptionMiddleware> logger, 
            IHostEnvironment environment
            )
        {
            _next = next;
            _logger = logger;
            _environment = environment;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex )
            {
                int internalErrorCode = (int)HttpStatusCode.InternalServerError;
                _logger.LogError(ex, ex.Message);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = internalErrorCode;

                string stackTraceResponse = ex.StackTrace is not null 
                    ? ex.StackTrace.ToString() 
                    : "No Stack provided";

                var response = _environment.IsDevelopment() 
                    ? new ApiException(internalErrorCode, ex.Message, stackTraceResponse)
                    :  new ApiException(internalErrorCode, ex.Message);


                var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

                var json = JsonSerializer.Serialize(response, options);

                await context.Response.WriteAsync(json);
            }
        }


    }
}
