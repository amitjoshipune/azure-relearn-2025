using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using TodoRequisition.WebAPI.Data;
using TodoRequisition.WebAPI.DTOs;
using TodoRequisition.WebAPI.Models;

namespace TodoRequisition.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoController : ControllerBase
    {
        private readonly TodoDbContext _db;
        public TodoController(TodoDbContext db) => _db = db;

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _db.TodoItems.ToListAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
          => await _db.TodoItems.FindAsync(id) is var t && t != null
               ? Ok(t) : NotFound();

        /*
        [HttpPost]
        public async Task<IActionResult> Create(TodoItem dto)
        {
            _db.TodoItems.Add(dto);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { dto.Id }, dto);
        }
        */

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TodoCreateDto dto)
        {
            if (!Enum.TryParse<TodoStatus>(dto.Status, true, out var parsedStatus))
                return BadRequest("Invalid status value");

            var entity = new TodoItem
            {
                Title = dto.Title,
                Description = dto.Description,
                ProductId = dto.ProductId,
                Status = parsedStatus
            };

            _db.TodoItems.Add(entity);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = entity.Id }, entity);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, TodoItem dto)
        {
            if (id != dto.Id) return BadRequest();
            var t = await _db.TodoItems.FindAsync(id);
            if (t == null) return NotFound();
            t.Title = dto.Title;
            t.Description = dto.Description;
            t.Status = dto.Status;
            t.ProductId = dto.ProductId;
            await _db.SaveChangesAsync();
            return NoContent();
        }

        // PATCH only status
        [HttpPatch("{id}/status")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] JsonElement body)
        {
            var t = await _db.TodoItems.FindAsync(id);
            if (t == null) return NotFound();
            if (body.TryGetProperty("status", out var st))
            {
                if (Enum.TryParse<TodoStatus>(st.GetString(), true, out var s))
                {
                    t.Status = s;
                }
                else return BadRequest("Invalid status");
            }
            await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var t = await _db.TodoItems.FindAsync(id);
            if (t == null) return NotFound();
            _db.TodoItems.Remove(t);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }


}
