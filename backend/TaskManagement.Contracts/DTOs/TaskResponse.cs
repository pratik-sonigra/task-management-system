namespace TaskManagement.Contracts.DTOs
{
    public class TaskResponse
    {
        public int TaskId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public int StatusId { get; set; }
        public string AssignedUser { get; set; }
        public int AssignedUserId { get; set; }
        public DateTime? DueDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
