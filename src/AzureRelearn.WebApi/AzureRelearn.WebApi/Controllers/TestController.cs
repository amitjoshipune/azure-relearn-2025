using Microsoft.AspNetCore.Mvc;

namespace AzureRelearn.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        // 🔹 This action will go through filter before middleware
        [HttpGet("filtered")]
        //[ServiceFilter(typeof(CustomExceptionFilter))]
        public IActionResult ThrowWithFilter()
        {
            throw new InvalidOperationException("Thrown inside controller action (Filter used)");
        }

        // 🔹 This will be caught by middleware
        [HttpGet("middleware")]
        public IActionResult ThrowWithoutFilter()
        {
            throw new Exception("Thrown inside controller action (No filter)");
        }

        // 🔹 This will be caught by built-in fallback (only if above two fail)
        [HttpGet("early")]
        public IActionResult EarlyError() =>
            throw new ApplicationException("Simulate exception in startup or routing (before middleware)?");
    }

}
