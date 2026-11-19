namespace Latypova
{
    internal class ProjectTask
    {
        public string Description;
        public DateTime Deadline;
        public Employee Initiator;
        public Employee Executor;
        public TaskStatus Status;
        public Report TaskReport;
        public ProjectTask(string description, DateTime deadline, Employee initiator, Employee executor)
        {
            Description = description;
            Deadline = deadline;
            Initiator = initiator;
            Executor = executor;
            Status = TaskStatus.Assigned;
        }
        public void TakeInWork()
        {
            Status = TaskStatus.InProgress;
        }
        public bool Delegate(Employee newExecutor)
        {
            if (Executor != null)
            {
                Executor = newExecutor;
                Status = TaskStatus.Assigned;
                return true;
            }
            Console.WriteLine("Никто не делегирует");
            return false;
        }
        public void Reject()
        {
            Executor = null;
        }
        public void Assign(Employee newExecutor)
        {
            Executor = newExecutor;
            Status = TaskStatus.Assigned;
        }
        public void SendReport(string reportText, Employee executor)
        {
            Report report = new Report(reportText, executor);
            TaskReport = report;
            Status = TaskStatus.Review;
        }
        public void ReportReview(bool isApproved)
        {
            if (isApproved == true)
            {
                Status = TaskStatus.Completed;
            }
            if (isApproved == false)
            {
                TaskReport = null;
                Status = TaskStatus.InProgress;
            }
        }
    }
}