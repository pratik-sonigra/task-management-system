namespace TaskManagement.Domain.Models;

public partial class TaskEntity
{
    public int TaskId { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public int? AssignedUserId { get; set; }

    public int StatusId { get; set; }

    public DateTime? DueDate { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int CreatedBy { get; set; }

    public virtual ICollection<ActivityLog> ActivityLogs { get; set; } = new List<ActivityLog>();

    public virtual User AssignedUser { get; set; }

    public virtual User CreatedByNavigation { get; set; }

    public virtual TaskStatus Status { get; set; }
}