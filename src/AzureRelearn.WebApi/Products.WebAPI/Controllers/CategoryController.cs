using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Products.WebAPI.Data;
using Products.WebAPI.DTOs;
using Products.WebAPI.Models;

namespace Products.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly AppDbContext _ctx;
        public CategoryController(AppDbContext ctx) => _ctx = ctx;

        [HttpGet("All", Name = "GetAllCategories")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _ctx.Categories.Select(c =>
            new CategoryDto
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description
            }).ToListAsync());

        }

        [HttpGet("{id:int}", Name = "GetCategoriesById")]
        public async Task<IActionResult> Get(int id)
        {
            /*
            var c = await _ctx.Categories.FindAsync(id);
            if (c == null) return NotFound();

            return Ok(new CategoryDto { Id = c.Id, Name = c.Name, Description = c.Description });
            */
            return await _ctx.Categories.FindAsync(id) is Category c 
                ? Ok(new CategoryDto { Id = c.Id, Name = c.Name, Description = c.Description }) : NotFound();
        }
    }
}
