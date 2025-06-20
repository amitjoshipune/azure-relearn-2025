using RequisitionProcessor.WebAPI.Models;

namespace RequisitionProcessor.WebAPI.DTOs
{
    public class TodoDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public string? Description { get; set; }
        public TodoStatus Status { get; set; }
        public int? ProductId { get; set; }
    }
}
