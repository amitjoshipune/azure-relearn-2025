namespace AzureRelearn.WebApi.Middlewares
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        public LoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task  Invoke(HttpContext context)
        {
            Console.WriteLine($"LoggingMiddleware {context.Request.Method} {context.Request.Path}");
            await _next(context);
            Console.WriteLine($"LoggingMiddleware {context.Response.StatusCode}");
        }
    }
}
