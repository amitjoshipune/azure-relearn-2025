
using Microsoft.EntityFrameworkCore;
using TodoRequisition.WebAPI.Data;

namespace TodoRequisition.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<TodoDbContext>(options =>
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

            builder.Services.AddControllers(
                // Register globally if desired
                // options.Filters.Add<CustomExceptionFilter>();
                );
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Add Custom Middleware

            // Map Controllers to ensure API endpoints work
            app.UseRouting();
            // Use CORS Middleware in Pipeline
            // Right after app.UseRouting();
            app.UseCors("AllowAll");

            
            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            //app.MapControllers();

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
            app.MapGet("/", () => "Hello from .NET 8 API with Health Checks + App Insights! TODO APIs");

            // Run the application
            app.Run();

        }
    }
}
