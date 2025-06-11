using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AzureRelearn.WebApi.Filters
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        public GlobalExceptionFilter()
        {

        }

        public void OnException(ExceptionContext context)
        {
            Console.WriteLine($"Exception caught in filter {context.Exception}");
            context.Result = new ObjectResult(
                new
                {
                    Message = "Filter handled the exception",
                    Detail = context.Exception.Message
                }
                )
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };

            context.ExceptionHandled = true; // prevent bubbling to middleware
        }
    }
}
