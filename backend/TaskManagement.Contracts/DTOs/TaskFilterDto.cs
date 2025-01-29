namespace TaskManagement.Contracts.DTOs
{
    public class TaskFilterDto
    {
        public string Title { get; set; }
        public int? StatusId { get; set; }
        public int? UserId { get; set; }
        public DateTime? DueDate { get; set; }
    }
}
