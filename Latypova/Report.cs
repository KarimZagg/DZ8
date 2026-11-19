namespace Latypova
{
    internal class Report
    {
        public string Text;
        public DateTime CompletionDate;
        public Employee Executor;
        public Report(string text, Employee executor)
        {
            Text = text;
            Executor = executor;
            CompletionDate = DateTime.Now;
        }
    }
}