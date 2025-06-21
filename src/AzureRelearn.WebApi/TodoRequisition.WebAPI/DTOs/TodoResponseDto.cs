namespace TodoRequisition.WebAPI.DTOs
{
    public class TodoResponseDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public string? Description { get; set; }
        public string Status { get; set; } = ""; // stringified status
        public int? ProductId { get; set; }
        public DateTime CreatedAtUtc { get; set; }
    }
}

