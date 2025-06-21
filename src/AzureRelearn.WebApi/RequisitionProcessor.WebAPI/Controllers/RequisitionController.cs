using Microsoft.AspNetCore.Mvc;
using RequisitionProcessor.WebAPI.DTOs;
using RequisitionProcessor.WebAPI.Models;

namespace RequisitionProcessor.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RequisitionController : ControllerBase
    {
        private readonly HttpClient _hc;
        public RequisitionController(IHttpClientFactory factory) => _hc = factory.CreateClient("default");

        [HttpPost("{todoId:int}/process")]
        public async Task<IActionResult> Process(int todoId)
        {
            var todoResp = await _hc.GetAsync($"http://todo/api/todo/{todoId}");
            if (!todoResp.IsSuccessStatusCode) return StatusCode((int)todoResp.StatusCode);

            var todo = await todoResp.Content.ReadFromJsonAsync<TodoDto>();
            if (todo?.ProductId != null)
            {
                var stockResp = await _hc.PostAsJsonAsync("http://stock/api/stock", new StockEventDto
                {
                    ProductId = todo.ProductId.Value,
                    EventType = StockEventType.Bought,
                    Quantity = 1
                });

                if (!stockResp.IsSuccessStatusCode)
                    return StatusCode((int)stockResp.StatusCode);
            }

            var patchResp = await _hc.PatchAsync($"http://todo/api/todo/{todoId}/status",
              JsonContent.Create(new { status = TodoStatus.Done }));

            return patchResp.IsSuccessStatusCode ? Ok() : StatusCode((int)patchResp.StatusCode);
        }
    }

}
