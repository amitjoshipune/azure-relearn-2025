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
            // todorequisiton web api -- http://localhost:5209
            //var todoResp = await _hc.GetAsync($"http://todo/api/todo/{todoId}");
            var todoResp = await _hc.GetAsync($"http://localhost:5209/api/todo/{todoId}");
            if (!todoResp.IsSuccessStatusCode) return StatusCode((int)todoResp.StatusCode);

            var todo = await todoResp.Content.ReadFromJsonAsync<TodoDto>();
            if (todo?.ProductId != null)
            {
                // stockevent web api -- http://localhost:5064
                /*
                var stockResp = await _hc.PostAsJsonAsync("http://stock/api/stock", new StockEventDto
                {
                    ProductId = todo.ProductId.Value,
                    EventType = StockEventType.Bought,
                    Quantity = 1
                });
                */
                var stockResp = await _hc.PostAsJsonAsync("http://localhost:5064/api/stock", new StockEventDto
                {
                    ProductId = todo.ProductId.Value,
                    EventType = StockEventType.Bought,
                    Quantity = 1
                });

                if (!stockResp.IsSuccessStatusCode)
                    return StatusCode((int)stockResp.StatusCode);
            }

            // todorequisiton web api -- http://localhost:5209
            /*
             var patchResp = await _hc.PatchAsync($"http://todo/api/todo/{todoId}/status",
              JsonContent.Create(new { status = TodoStatus.Done }));
            */

            var patchResp = await _hc.PatchAsync($"http://localhost:5209/api/todo/{todoId}/status",
              JsonContent.Create(new { status = TodoStatus.Done }));
            return patchResp.IsSuccessStatusCode ? Ok() : StatusCode((int)patchResp.StatusCode);
        }
    }

}
