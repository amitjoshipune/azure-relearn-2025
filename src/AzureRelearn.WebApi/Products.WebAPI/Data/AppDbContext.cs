using Microsoft.EntityFrameworkCore;
using Products.WebAPI.Models;

namespace Products.WebAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        /*

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Categories
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Oils", Description = "Cooking & edible oils" },
                new Category { Id = 2, Name = "Dairy", Description = "Milk, Butter, Curd" },
                new Category { Id = 3, Name = "Spices", Description = "Cooking spices" },
                new Category { Id = 4, Name = "Staples", Description = "Daily essential staples" },
                new Category { Id = 5, Name = "Vegetables", Description = "Fresh vegetables" },
                new Category { Id = 6, Name = "Bakery", Description = "Bakery & bread products" }
            );

            // Products
            var products = new[]
            {
        ("Washing Soap", 4), ("Sugar", 4), ("Rice Bran Oil",1), ("Vim Soap Bar",4),
        ("Salt",4), ("Tea Powder",4), ("Jaggery",4), ("Bathing Soap",4), ("Rice",4),
        ("Wheat Flour",4), ("Red Chilli Powder",3), ("Rice Flakes",4), ("Coriander",3),
        ("Green Chilli",5), ("Ginger",5), ("Onion",5), ("Tomato",5), ("Lemon",5),
        ("Potato",5), ("Amul Butter",2), ("Curd",2), ("Buffalo Milk",2),
        ("Cow Ghee",2), ("Govardhan Cow Ghee",2), ("Brown Bread",6),
        ("Lady Fingers",5), ("Cumin Seeds",3), ("Cabbage",5), ("Cheese Cube",2)
    };

            int id = 1;
            foreach (var (name, catId) in products)
            {
                modelBuilder.Entity<Product>().HasData(
                    new Product
                    {
                        Id = id++,
                        Name = name,
                        Description = "", // you may fill as needed
                        CategoryId = catId
                    });
            }
        }

        */
    }
}
