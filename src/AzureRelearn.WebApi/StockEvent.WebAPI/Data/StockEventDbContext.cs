using Microsoft.EntityFrameworkCore;
using StockEvent.WebAPI.Models;

namespace StockEvent.WebAPI.Data
{
    public class StockEventDbContext :DbContext
    {
        public DbSet<StockEventItem> StockEventItems { get; set; }
    }
}
