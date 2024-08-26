using Microsoft.OData;
using Newtonsoft.Json;
using System.Net;
namespace Ligl.LegalManagement.Api.Middleware
{
    public static    class ExceptionHandlingMiddleWare
    {
        public static IApplicationBuilder UseExceptionHandlingMiddleWare(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandling>();
        }
    }


    /// <summary>
    /// Custom Exception Handling Middleware. Catches every single exception from the request pipeline.
    /// </summary>
    /// <remarks>
    /// Exception Handling Middleware Constructor
    /// </remarks>
    /// <param name="next"></param>
    /// <param name="logger"></param>
    public class ExceptionHandling(RequestDelegate next, ILogger<ExceptionHandling> logger)
    {
        private readonly RequestDelegate _next = next;
        private ILogger<ExceptionHandling> _logger = logger;

        private const string ClassName = nameof(ExceptionHandling);

        /// <summary>
        ///
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="logger"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext httpContext, ILogger<ExceptionHandling> logger)
        {
            const string methodName = $"{ClassName} - {nameof(Invoke)}";
            _logger = logger;
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    $"{methodName} - Exception occurred in  Request-Response Pipeline. Message : {ex.Message}, Stacktrace : {ex.StackTrace}",
                    ex);
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        /// <summary>
        /// Handling Exceptions
        /// </summary>
        /// <param name="context"></param>
        /// <param name="exception"></param>
        /// <returns></returns>
        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            const string methodName = $"{ClassName} - nameof(HandleExceptionAsync)";
            // All custom exceptions -> http status code handling to go here.
            var code = HttpStatusCode.InternalServerError;

            // Distinguish all custom errors thrown and set appropriate response status code.
            // eg, Unauthorized, validation fails etc.
            var customErrorObject = new ODataError();
            if (exception is { } cException)
            {
                code = HttpStatusCode.BadRequest;
                customErrorObject.ErrorCode = cException.ToString();
                customErrorObject.Message = string.Format(cException.Message, cException.ToString());
            }
            else
            {
                customErrorObject.InnerError = new ODataInnerError(exception);
            }

            var result = JsonConvert.SerializeObject(customErrorObject);

            _logger.LogInformation($"{methodName} - Error Response Object : {result}");

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }
    }
}
