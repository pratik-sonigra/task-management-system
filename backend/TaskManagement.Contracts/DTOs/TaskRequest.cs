namespace TaskManagement.Contracts.DTOs
{
    public class TaskRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int StatusId { get; set; }
        public int AssignedUserId { get; set; }
        public DateTime? DueDate { get; set; }
        public int CreatedBy { get; set; }
    }
}
