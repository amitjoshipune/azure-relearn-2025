using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using TodoRequisition.WebAPI.Data;
using TodoRequisition.WebAPI.DTOs;
using TodoRequisition.WebAPI.Models;

[ApiController]
[Route("api/[controller]")]
public class TodoController : ControllerBase
{
    private readonly TodoDbContext _db;
    public TodoController(TodoDbContext db) => _db = db;

    [HttpGet("allwithoutpaging", Name = "GetAllWithoutPaging")]
    public async Task<IActionResult> GetAll()
    {
        var todos = await _db.TodoItems.ToListAsync();
        var result = todos.Select(t => new TodoResponseDto
        {
            Id = t.Id,
            Title = t.Title,
            Description = t.Description,
            Status = t.Status.ToString(),
            ProductId = t.ProductId,
            CreatedAtUtc = t.CreatedAtUtc
        });
        return Ok(result);
    }

    [HttpGet("all",Name ="GetAllWithPaging")]
    public async Task<IActionResult> GetAll([FromQuery] TodoQueryParams q)
    {
        var build = _db.TodoItems.AsQueryable();

        // filters
        if (q.From.HasValue)
            build = build.Where(t => t.CreatedAtUtc >= q.From.Value);
        if (q.To.HasValue)
            build = build.Where(t => t.CreatedAtUtc <= q.To.Value);

        // ordered and paged
        var total = await build.CountAsync();
        var items = await build
          .OrderByDescending(t => t.CreatedAtUtc)
          .Skip((q.Page - 1) * q.PageSize)
          .Take(q.PageSize)
          .Select(t => new TodoResponseDto
          {
              Id = t.Id,
              Title = t.Title,
              Description = t.Description,
              Status = t.Status.ToString(),
              ProductId = t.ProductId,
              CreatedAtUtc = t.CreatedAtUtc
          })
          .ToListAsync();

        return Ok(new { total, items });
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var t = await _db.TodoItems.FindAsync(id);
        return t == null ? NotFound() : Ok(new TodoResponseDto
        {
            Id = t.Id,
            Title = t.Title,
            Description = t.Description,
            Status = t.Status.ToString(),
            ProductId = t.ProductId,
            CreatedAtUtc = t.CreatedAtUtc
        });
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] TodoCreateDto dto)
    {
        if (!Enum.TryParse<TodoStatus>(dto.Status, true, out var parsedStatus))
            return BadRequest("Invalid status value.");

        var entity = new TodoItem
        {
            Title = dto.Title,
            Description = dto.Description,
            ProductId = dto.ProductId,
            Status = parsedStatus
        };

        _db.TodoItems.Add(entity);
        await _db.SaveChangesAsync();

        var result = new TodoResponseDto
        {
            Id = entity.Id,
            Title = entity.Title,
            Description = entity.Description,
            Status = entity.Status.ToString(),
            ProductId = entity.ProductId,
            CreatedAtUtc = entity.CreatedAtUtc
        };

        return CreatedAtAction(nameof(Get), new { id = entity.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] TodoUpdateDto dto)
    {
        if (id != dto.Id) return BadRequest();
        var t = await _db.TodoItems.FindAsync(id);
        if (t == null) return NotFound();

        if (!Enum.TryParse<TodoStatus>(dto.Status, true, out var parsedStatus))
            return BadRequest("Invalid status value.");

        t.Title = dto.Title;
        t.Description = dto.Description;
        t.Status = parsedStatus;
        t.ProductId = dto.ProductId;

        await _db.SaveChangesAsync();
        return NoContent();
    }

    [HttpPatch("{id}/status")]
    public async Task<IActionResult> UpdateStatus(int id, [FromBody] JsonElement body)
    {
        var t = await _db.TodoItems.FindAsync(id);
        if (t == null) return NotFound();

        if (body.TryGetProperty("status", out var st))
        {
            if (!Enum.TryParse<TodoStatus>(st.GetString(), true, out var parsed))
                return BadRequest("Invalid status.");

            t.Status = parsed;
            await _db.SaveChangesAsync();
            return NoContent();
        }

        return BadRequest("Missing 'status' field.");
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
