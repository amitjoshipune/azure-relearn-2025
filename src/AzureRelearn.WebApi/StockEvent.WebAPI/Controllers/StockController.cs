using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StockEvent.WebAPI.Data;
using StockEvent.WebAPI.DTOs;
using StockEvent.WebAPI.Models;

namespace StockEvent.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StockController : ControllerBase
    {
        private readonly StockEventDbContext _ctx;
        public StockController(StockEventDbContext context)
        {
            _ctx = context;
        }
        /* AddEvent (POST), GetEvents, GetCurrentStock */
        [HttpGet("{id:int}", Name = nameof(GetById))]
        public async Task<IActionResult> GetById(int id)
        {
            return await _ctx.StockEventItems.FindAsync(id) is StockEventItem evt ? Ok(evt) : NotFound();
        }

        public async Task<IActionResult> Post(StockEventDto dto)
        {
            var e = new StockEventItem
            {
                ProductId = dto.ProductId,
                EventType = dto.EventType,
                Quantity = dto.Quantity
            };
            _ctx.StockEventItems.Add(e);
            await _ctx.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = e.Id }, e);
            //return Created($"api/stock/{e.Id}", e);
        }

        [HttpGet("events/{productId}")]
        public async Task<IActionResult> GetEvents(int productId)
        {
            var list = await _ctx.StockEventItems
                              .Where(e => e.ProductId == productId)
                              .OrderByDescending(e => e.EventAtUtc)
                              .ToListAsync();
            return Ok(list);
        }

        [HttpGet("current/{productId}")]
        public async Task<IActionResult> GetCurrent(int productId)
        {
            var qty = await _ctx.StockEventItems
                                .Where(e => e.ProductId == productId)
                                .SumAsync(e => e.EventType == StockEventType.Bought ? e.Quantity : -e.Quantity);
            return Ok(new { productId, stockOnHand = qty });
        }
    }
}
