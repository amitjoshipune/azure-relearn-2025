using AzureRelearn.WebApi.DTOs;
using AzureRelearn.WebApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace AzureRelearn.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly JwtService _jwtService;

        public AuthController(JwtService jwtService)
        {
            _jwtService = jwtService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequestDto request)
        {
            // In real life, check against DB or user store
            if ((request.Username == "admin" && request.Password == "password") || (request.Username == "amitjoshi" && request.Password == "amitjoshi@12345"))
            {
                var token = _jwtService.GenerateToken(request.Username);
                return Ok(new { token });
            }

            return Unauthorized();
        }

        [Authorize]
        [HttpGet("secure")]
        public IActionResult SecureEndpoint()
        {
            var username = User.Identity?.Name ?? "Unknown";
            return Ok(new { message = $"Hello {username}, you're authorized!" });
        }

        [HttpPost("validate")]
        public IActionResult ValidateToken([FromBody] string token)
        {
            var principal = _jwtService.ValidateToken(token);
            if (principal != null)
            {
                return Ok(new { message = "Token is valid" });
            }

            return Unauthorized();
        }

        [HttpPost("refresh")]
        public IActionResult RefreshToken([FromBody] string oldToken)
        {
            var principal = _jwtService.ValidateToken(oldToken);
            if (principal == null)
                return Unauthorized();

            var username = principal.Identity?.Name ?? "Unknown";
            var newToken = _jwtService.GenerateToken(username);
            return Ok(new { token = newToken });
        }
    }
}
