using Microsoft.ApplicationInsights.WindowsServer;

namespace AzureRelearn.WebApi.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex) 
            {
                Console.WriteLine($"Global Middleware Caught Exception. {ex}");

                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType ="application/json";

                var response = new { Message ="Middleware Handled Error", Details = ex.Message};

                await context.Response.WriteAsJsonAsync(response);
            }
        }
    }
}
