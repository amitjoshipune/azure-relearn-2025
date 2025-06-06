using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace AzureRelearn.WebApi.Data
{
    public class MySqlDbContext : DbContext
    {
        public MySqlDbContext(DbContextOptions<MySqlDbContext> options) :base (options)
        {
            
        }
        public DbSet<WeatherForecast> WeatherForecasts { get; set; } 
    }
}
