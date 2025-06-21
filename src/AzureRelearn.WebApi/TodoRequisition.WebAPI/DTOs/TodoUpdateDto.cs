namespace TodoRequisition.WebAPI.DTOs
{
    public class TodoUpdateDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public string? Description { get; set; }
        public string Status { get; set; } = "Pending";
        public int? ProductId { get; set; }
    }
}
