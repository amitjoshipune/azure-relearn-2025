using AzureRelearn.WebApi.Data;
using AzureRelearn.WebApi.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using OpenTelemetry.Resources;

using OpenTelemetry.Trace;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// App Insights
builder.Services.AddApplicationInsightsTelemetry();

// **Configure OpenTelemetry**
builder.Services.AddOpenTelemetry()
    .WithTracing( traceProviderBuilder =>
    {
        object value = traceProviderBuilder
        .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("AzureRelearn Web API"))
        .SetSampler(new AlwaysOnSampler()) // Ensures all traces are recorded
        .AddAspNetCoreInstrumentation() // Required for EF Core instrumentation
        .AddEntityFrameworkCoreInstrumentation()
        .AddConsoleExporter()
        .AddOtlpExporter(opt=>
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
         var result =  JsonSerializer.Serialize(new
         {
            status = report.Status.ToString(),
            checks = report.Entries.Select( e => new
            {
                name = e.Key,      
                status = e.Value.Status.ToString(),
                exception = e.Value.Exception?.Message
            })
         })  ;
         context.Response.ContentType = "application/json";
         await context.Response.WriteAsync(result);
     }
});


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
