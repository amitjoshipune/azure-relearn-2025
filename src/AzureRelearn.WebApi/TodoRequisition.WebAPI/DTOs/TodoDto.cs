using TodoRequisition.WebAPI.Models;

namespace TodoRequisition.WebAPI.DTOs
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
