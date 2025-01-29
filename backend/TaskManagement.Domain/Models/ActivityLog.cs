namespace TaskManagement.Domain.Models;

public partial class ActivityLog
{
    public int LogId { get; set; }

    public int TaskId { get; set; }

    public int UpdatedBy { get; set; }

    public int ChangeTypeId { get; set; }

    public DateTime? ChangeDate { get; set; }

    public virtual ChangeType ChangeType { get; set; }

    public virtual TaskEntity Task { get; set; }

    public virtual User UpdatedByNavigation { get; set; }
}