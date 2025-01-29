namespace TaskManagement.Domain.Models;

public partial class ChangeType
{
    public int ChangeTypeId { get; set; }

    public string ChangeTypeName { get; set; }

    public virtual ICollection<ActivityLog> ActivityLogs { get; set; } = new List<ActivityLog>();
}