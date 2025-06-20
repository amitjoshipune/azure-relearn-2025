namespace TodoRequisition.WebAPI.Models
{
    public enum TodoStatus { Pending, Done, OnHold, Cancelled }

    public class TodoItem
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public string? Description { get; set; }
        public TodoStatus Status { get; set; } = TodoStatus.Pending;
        public int? ProductId { get; set; }  // optional product reference
        public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
    }
}
