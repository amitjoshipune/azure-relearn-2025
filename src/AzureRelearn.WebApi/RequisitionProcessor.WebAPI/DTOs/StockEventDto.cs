using RequisitionProcessor.WebAPI.Models;

namespace RequisitionProcessor.WebAPI.DTOs
{
    public class StockEventDto
    {
        public int ProductId { get; set; }
        public StockEventType EventType { get; set; }
        public int Quantity { get; set; }
    }
}
