namespace Latypova
{
    internal class Project
    {
        public string Description;
        public DateTime Deadline;
        public Employee Customer;
        public Employee TeamLead;
        public List<ProjectTask> Tasks;
        public ProjectStatus Status;

        public Project(string description, DateTime deadline, Employee customer, Employee teamLead)
        {
            Description = description;
            Deadline = deadline;
            Customer = customer;
            TeamLead = teamLead;
            Tasks = new List<ProjectTask>();
            Status = ProjectStatus.Project;
        }

        public void AddTask(Employee creator, ProjectTask task)
        {
            if (creator != TeamLead)
            {
                Console.WriteLine($"Ошибка: только тимлид ({TeamLead.Name}) может назначать задачи.");
                return;
            }

            if (Status != ProjectStatus.Project)
            {
                Console.WriteLine("Ошибка: нельзя добавлять задачи, проект уже переведён в исполнение или закрыт.");
                return;
            }

            Tasks.Add(task);
            Console.WriteLine($"Задача «{task.Description}» добавлена в проект (Исполнитель: {task.Executor.Name}).");
        }

        public void DeleteTask(Employee creator, ProjectTask task)
        {
            if (creator == TeamLead && task.Executor == null)
            {
                Tasks.Remove(task);
                Console.WriteLine($"Задача «{task.Description}» удалена из проекта.");
            }
            else
            {
                Console.WriteLine("Ошибка: удалить можно только отклонённую задачу и только тимлидом.");
            }
        }

        public void StartExecution()
        {
            if (Status == ProjectStatus.Project && Tasks.Count > 0)
            {
                Status = ProjectStatus.Execution;
                Console.WriteLine($"Проект «{Description}» переведён в статус: Исполнение.");
            }
            else
            {
                Console.WriteLine("Ошибка: нельзя перевести проект в исполнение (нет задач или неверный статус).");
            }
        }

        public bool CloseProject()
        {
            if (Tasks.Count == 0)
            {
                return false;
            }

            foreach (ProjectTask task in Tasks)
            {
                if (task.Status != TaskStatus.Completed)
                {
                    Console.WriteLine($"Проект «{Description}» пока нельзя закрыть: не все задачи выполнены.");
                    return false;
                }
            }

            Status = ProjectStatus.Closed;
            Console.WriteLine($"Все задачи выполнены! Проект «{Description}» переведён в статус: Закрыт.");
            return true;
        }
    }
}