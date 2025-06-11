using AzureRelearn.WebApi.Data;
using AzureRelearn.WebApi.Filters;
using AzureRelearn.WebApi.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using OpenTelemetry.Resources;

using OpenTelemetry.Trace;
using System.Text;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// App Insights
builder.Services.AddApplicationInsightsTelemetry();

// **Configure OpenTelemetry**
builder.Services.AddOpenTelemetry()
    .WithTracing(traceProviderBuilder =>
    {
        object value = traceProviderBuilder
        .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("AzureRelearn Web API"))
        .SetSampler(new AlwaysOnSampler()) // Ensures all traces are recorded
        .AddAspNetCoreInstrumentation() // Required for EF Core instrumentation
        .AddEntityFrameworkCoreInstrumentation()
        .AddConsoleExporter()
        .AddOtlpExporter(opt =>
        {
            opt.Endpoint = new Uri("http://localhost:4318");
        });

    }
    );

builder.Services.AddDbContext<MySqlDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
    );

// Health Checks
builder.Services.AddHealthChecks()
    .AddCheck("self", () => HealthCheckResult.Healthy())
    .AddSqlServer(builder.Configuration.GetConnectionString("DefaultConnection") ?? "");

// JWT related code -- Start
// JWT Config
var jwtKey = "this_is_my_super_secure_key_12345";
var issuer = "myapi";
var audience = "myapi-users";

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = issuer,
        ValidAudience = audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
    };
});
// JWT related code -- End

// Add Controllers for API functionality
builder.Services.AddControllers(
    // Register globally if desired
    // options.Filters.Add<CustomExceptionFilter>();
    );

builder.Services.AddScoped<GlobalExceptionFilter>();

builder.Services.AddLogging();

// Swagger Configuration (Available in all environments)
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "AzureRelearn Web API",
        Version = "v1"
    });
});

// Build the application
var app = builder.Build();

/*
//  1. Built-in exception handler fallback
app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        var feature = context.Features.Get<IExceptionHandlerFeature>();
        if (feature != null)
        {
            context.Response.StatusCode = 500;
            context.Response.ContentType = "application/json";

            await context.Response.WriteAsJsonAsync(new
            {
                Message = "Fallback Handler Caught It",
                Detail = feature.Error.Message
            });
        }
    });
});

*/

//  2. Custom Middleware
app.UseMiddleware<ExceptionHandlingMiddleware>();


app.UseMiddleware<LoggingMiddleware>();
// this fix suggested by chatgpt but not added this.
//app.Urls.Add("http://0.0.0.0:8080"); // Ensure API is accessible inside Docker

// Map Health Check endpoint
//app.MapHealthChecks("/healthz"); // this will work but when unhealthy simply returns string. 
// To get formatted JSON response adding following code
app.MapHealthChecks("/healthz", new HealthCheckOptions
{
    ResponseWriter = async (context, report) =>
    {
        var result = JsonSerializer.Serialize(new
        {
            status = report.Status.ToString(),
            checks = report.Entries.Select(e => new
            {
                name = e.Key,
                status = e.Value.Status.ToString(),
                exception = e.Value.Exception?.Message
            })
        });
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsync(result);
    }
});



// Map Controllers to ensure API endpoints work
app.UseRouting();

/*
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
*/

/*
  Why these changes work
app.MapControllers() handles route registration and automatically injects the needed 
UseEndpoints(...) middleware behind the scenes 

Explicit removal of UseEndpoints(...) resolves ASP0014.

Correct order of UseRouting() -> UseAuthentication() -> UseAuthorization() -> endpoints ensures authorization is applied properly, resolving ASP0001 

Summary
Warning	Fix
ASP0001: "UseAuthorization should appear between routing and endpoints"	Put UseAuthorization() after UseRouting() and before route mapping.
ASP0014: "Suggest using top-level route registrations instead of UseEndpoints"	Replace app.UseEndpoints(...) with app.MapControllers() and route maps.
 */
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

// Serve Swagger UI
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Default route for simple testing
app.MapGet("/", () => "Hello from .NET 8 API with Health Checks + App Insights!");

// Run the application
app.Run();
