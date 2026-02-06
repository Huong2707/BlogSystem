using System.Net;
using System.Text.Json;

namespace BlogSystem.API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _logger = logger;
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {

            }
            catch (Exception ex)
            {
               _logger.LogError(ex, "An Error Exception");
                await HandleExceptionAsync(context, ex);
            }
        }
        private static Task HandleExceptionAsync(HttpContext context,Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode=(int)HttpStatusCode.InternalServerError;
            var response = new { 
                success= false,
                message="An error exception",
                error=exception.ToString(),
                timestamp = DateTime.UtcNow
            };
            var jsonResponse = JsonSerializer.Serialize(response);

            return context.Response.WriteAsync(jsonResponse);

        }
      
    }
}
