using Microsoft.AspNetCore.Mvc;
using RequisitionProcessor.WebAPI.DTOs;
using RequisitionProcessor.WebAPI.Models;

namespace RequisitionProcessor.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RequisitionController : ControllerBase
    {
        private readonly IHttpClientFactory _hc;
        public RequisitionController(IHttpClientFactory hc) => _hc = hc;

        [HttpPost("{todoId}/process")]
        public async Task<IActionResult> Process(int todoId)
        {
            var hc = _hc.CreateClient();
            var todo = await hc.GetFromJsonAsync<TodoDto>($"http://todo/api/todo/{todoId}");
            if (todo == null) return NotFound();

            if (todo.ProductId.HasValue)
            {
                await hc.PostAsJsonAsync("http://stock/api/stock", new StockEventDto
                {
                    ProductId = todo.ProductId.Value,
                    EventType = StockEventType.Bought,
                    Quantity = 1
                });
            }

            await hc.PatchAsync($"http://todo/api/todo/{todoId}/status",
                JsonContent.Create(new { status = TodoStatus.Done }));

            return Ok();
        }
    }

}
