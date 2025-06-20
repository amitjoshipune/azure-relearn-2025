namespace StockEvent.WebAPI.Models
{
    public enum StockEventType { Bought, Consumed, Lost, Gifted }

    public class StockEventItem
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public StockEventType EventType { get; set; }
        public int Quantity { get; set; }
        public DateTime EventAtUtc { get; set; } = DateTime.UtcNow;
    }

}
