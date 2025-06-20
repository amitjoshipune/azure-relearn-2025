using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using TodoRequisition.WebAPI.Models;

namespace TodoRequisition.WebAPI.Data
{
    public class TodoDbContext : DbContext
    {
        public DbSet<TodoItem> TodoItems { get; set; }
        public TodoDbContext(DbContextOptions options) :base(options)
        {
            
        }
    }

}
