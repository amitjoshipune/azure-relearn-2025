using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Products.WebAPI.Data;
using Products.WebAPI.DTOs;
using Products.WebAPI.Models;
using System.Text.Json;

namespace Products.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ApiExplorerSettings(GroupName ="v1")]
    public class ProductController :ControllerBase
    {
        private string[] productMaster = new[] { "Washing Soap", "Sugar", "Rice Bran Oil" , "Vim Soap Bar"
            ,"Salt", "Tea Powder" , "Jaggery" , "Bathing Soap" , "Rice", "Wheat Floor", "Red Chilli Powder",
            "Rice flakes" , "Corriander" , "Green Chilli", "Ginger", "Onion", "Tomato", "Lemon", "Potato",
            "Amul Butter", "Curd","Buffalo Milk", "Cow Ghee" , "Govardhan Cow Ghee", "Brown Bread", "Lady Fingers",
            "Cumin Seeds", "Cabbage", "Cheese Cube"};

        private readonly AppDbContext _ctx;
        public ProductController(AppDbContext _appDbContext) 
        {
            _ctx = _appDbContext;
        }

        [HttpGet("All", Name ="GetAllProducts")]
        public async Task<IActionResult> GetProducts(int page = 1, int pageSize = 10, int? categoryId = null)
        {
            var query = _ctx.Products.Include(p=>p.Category).AsQueryable();

            if (categoryId.HasValue) query = query.Where(p => p.CategoryId == categoryId);
            var total = await query.CountAsync();
            var items = await query.Skip((page - 1) * pageSize).Take(pageSize)
                                   .Select(p => new ProductDto
                                   {
                                       Id = p.Id,
                                       Name = p.Name,
                                       Description = p.Description,
                                       Category = p.Category != null ? new CategoryDto
                                       {
                                           Id = p.Category.Id,
                                           Name = p.Category.Name,
                                           Description = p.Category.Description
                                       } : null
                                   }).ToListAsync();
            return Ok(new { total, items });
        }

        // GET by Id
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var p = await _ctx.Products.Include(p => p.Category)
              .FirstOrDefaultAsync(p => p.Id == id);
            if (p == null) return NotFound();
            return Ok(new ProductDto {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Category = p.Category != null ? new CategoryDto
                {
                    Id = p.Category.Id,
                    Name = p.Category.Name,
                    Description = p.Category.Description
                } : null

            });
        }

        // GET by category name
        [HttpGet("byCategory")]
        public async Task<IActionResult> ByCategory(string name)
        {
            var items = await _ctx.Products
              .Include(p => p.Category)
              .Where(p => p.Category != null && p.Category.Name == name)
              .Select(p => new ProductDto {
                  Id = p.Id,
                  Name = p.Name,
                  Description = p.Description,
                  Category = p.Category != null ? new CategoryDto
                  {
                      Id = p.Category.Id,
                      Name = p.Category.Name,
                      Description = p.Category.Description
                  } : null
              })
              .ToListAsync();
            return Ok(items);
        }

        // POST
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductDto dto)
        {
            var p = new Product
            {
                Name = dto.Name,
                Description = dto.Description,
                CategoryId = dto.Category?.Id
            };
            _ctx.Products.Add(p);
            await _ctx.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = p.Id }, p);
        }

        // PUT
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] ProductDto dto)
        {
            var p = await _ctx.Products.FindAsync(id);
            if (p == null) return NotFound();
            p.Name = dto.Name;
            p.Description = dto.Description;
            p.CategoryId = dto.Category?.Id;
            await _ctx.SaveChangesAsync();
            return NoContent();
        }

        // PATCH
        [HttpPatch("{id:int}")]
        public async Task<IActionResult> Patch(int id, [FromBody] JsonElement patch)
        {
            var p = await _ctx.Products.FindAsync(id);
            if (p == null) return NotFound();
            if (patch.TryGetProperty("Name", out var name)) p.Name = name.GetString()!;
            if (patch.TryGetProperty("Description", out var desc)) p.Description = desc.GetString();
            await _ctx.SaveChangesAsync();
            return NoContent();
        }

        // DELETE
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var p = await _ctx.Products.FindAsync(id);
            if (p == null) return NotFound();
            _ctx.Products.Remove(p);
            await _ctx.SaveChangesAsync();
            return NoContent();
        }
    }
}
