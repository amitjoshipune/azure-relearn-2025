using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// App Insights
builder.Services.AddApplicationInsightsTelemetry();

// Health Checks
builder.Services.AddHealthChecks();

// Add Controllers for API functionality
builder.Services.AddControllers();

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
//app.Urls.Add("http://0.0.0.0:8080"); // Ensure API is accessible inside Docker

// Map Health Check endpoint
app.MapHealthChecks("/healthz");

// Map Controllers to ensure API endpoints work
app.UseRouting();
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
