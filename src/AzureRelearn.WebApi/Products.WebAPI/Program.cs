
using Microsoft.EntityFrameworkCore;
using Products.WebAPI.Data;

namespace Products.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
            });

            // Enable CORS -
            // Register CORS Policy in builder.Services
            // define policy -- Place it right before builder.Services.AddControllers().
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
            });

            // Add Controllers for API functionality
            builder.Services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // add Custom Middleware over here

            //// Configure the HTTP request pipeline.
            //if (app.Environment.IsDevelopment() || app.Environment.IsProduction() || app.Environment.IsStaging())
            //{
            //    app.UseSwagger();
            //    app.UseSwaggerUI();
            //}

            // Map Controllers to ensure API endpoints work
            app.UseRouting();
            // Use CORS Middleware in Pipeline
            // Right after app.UseRouting();
            app.UseCors("AllowAll");

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // Serve Swagger UI
            if (app.Environment.IsDevelopment() || app.Environment.IsProduction() || app.Environment.IsStaging())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // Default route for simple testing
            app.MapGet("/", () => "Hello from .NET 8 API with Health Checks + App Insights!");

            // Run the application
            app.Run();
        }
    }
}
