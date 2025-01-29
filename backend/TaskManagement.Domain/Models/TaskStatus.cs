namespace TaskManagement.Domain.Models;

public partial class TaskStatus
{
    public int StatusId { get; set; }

    public string StatusName { get; set; }

    public virtual ICollection<TaskEntity> Tasks { get; set; } = new List<TaskEntity>();
}