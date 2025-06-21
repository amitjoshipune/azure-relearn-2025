using Microsoft.EntityFrameworkCore;
using StockEvent.WebAPI.Models;

namespace StockEvent.WebAPI.Data
{
    public class StockEventDbContext :DbContext
    {
        public StockEventDbContext(DbContextOptions<StockEventDbContext> options):base(options)
        {
            
        }
        public DbSet<StockEventItem> StockEventItems { get; set; }
    }
}
