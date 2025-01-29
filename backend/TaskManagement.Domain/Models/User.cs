namespace TaskManagement.Domain.Models;

public partial class User
{
    public int UserId { get; set; }

    public string Username { get; set; }

    public string PasswordHash { get; set; }

    public string Email { get; set; }

    public int RoleId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<ActivityLog> ActivityLogs { get; set; } = new List<ActivityLog>();

    public virtual Role Role { get; set; }

    public virtual ICollection<TaskEntity> TaskAssignedUsers { get; set; } = new List<TaskEntity>();

    public virtual ICollection<TaskEntity> TaskCreatedByNavigations { get; set; } = new List<TaskEntity>();
}