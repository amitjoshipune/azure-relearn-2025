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
            Console.WriteLine("******************* Before next --> ***************************");
            Console.WriteLine($"LoggingMiddleware {context.Request.Method} {context.Request.Path}");
            Console.WriteLine("******************* Before next <-- ***************************");
            await _next(context);
            Console.WriteLine("******************** After next --> ***************************");
            Console.WriteLine($"LoggingMiddleware {context.Response.StatusCode}");
            Console.WriteLine("******************** After next <-- ***************************");
        }
    }
}
