namespace TaskManagement.Contracts.DTOs
{
    public class DashboardSummary
    {
        public int TotalTasks { get; set; }
        public List<TaskStatusCount> TasksByStatus { get; set; }
        public List<UserTaskCount> TasksPerUser { get; set; }
    }

    public class TaskStatusCount
    {
        public string Status { get; set; }
        public int Count { get; set; }
    }

    public class UserTaskCount
    {
        public string UserName { get; set; }
        public int TaskCount { get; set; }
    }

}
