namespace TodoRequisition.WebAPI.DTOs
{
    public class TodoCreateDto
    {
        public string Title { get; set; } = "";
        public string? Description { get; set; }
        public string Status { get; set; } = "Pending"; // Accept string and parse manually
        public int? ProductId { get; set; }
    }
}
