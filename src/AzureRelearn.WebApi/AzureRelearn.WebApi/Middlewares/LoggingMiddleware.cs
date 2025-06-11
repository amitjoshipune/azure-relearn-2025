namespace AzureRelearn.WebApi.Middlewares
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private ILogger<LoggingMiddleware> _logger;
        public LoggingMiddleware(RequestDelegate next , ILogger<LoggingMiddleware> logger)
        {
            _next = next;
           _logger = logger;
        }

        public async Task  Invoke(HttpContext context)
        {
            Console.WriteLine("******************* Before next --> ***************************");
            _logger.LogInformation("******************* Before next --> ***************************");
            Console.WriteLine($"LoggingMiddleware {context.Request.Method} {context.Request.Path}");
            _logger.LogInformation($"LoggingMiddleware {context.Request.Method} {context.Request.Path}");
            Console.WriteLine("******************* Before next <-- ***************************");
            _logger.LogInformation("******************* Before next <-- ***************************");
            await _next(context);
            Console.WriteLine("******************** After next --> ***************************");
            _logger.LogInformation("******************** After next --> ***************************");
            Console.WriteLine($"LoggingMiddleware {context.Response.StatusCode}");
            _logger.LogInformation($"LoggingMiddleware {context.Response.StatusCode}");
            Console.WriteLine("******************** After next <-- ***************************");
            _logger.LogInformation("******************** After next <-- ***************************");
        }
    }
}
