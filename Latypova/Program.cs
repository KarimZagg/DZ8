namespace Latypova
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Программа для работы IT-команды из 10 человек над проектом.\n");

            Employee customer = new Employee("Сережа", "Заказчик");

            List<Employee> team = new List<Employee>
            {
                new Employee("Карим", "Тимлид"),
                new Employee("Андрей", "Разработчик"),
                new Employee("Данияр", "Разработчик"),
                new Employee("Алииса", "Разработчик"),
                new Employee("Лиза", "Разработчик"),
                new Employee("Адель", "Разработчик"),
                new Employee("Амир", "Разработчик"),
                new Employee("Рушан", "Разработчик"),
                new Employee("Алдияр", "Разработчик"),
                new Employee("Григорий", "Разработчик")
            };

            Employee teamLead = team[0];

            Project project = new Project(
                "Сделать убыточный AI B2b Saas startup",
                DateTime.Now.AddMonths(2),
                customer,
                teamLead
            );

            Console.WriteLine($"Создан новый проект: \"{project.Description}\". Текущий статус: {project.Status}.\n");

            ProjectTask task1 = new ProjectTask("Давать звездюлей бестолочам", DateTime.Now.AddDays(14), teamLead, team[0]);
            ProjectTask task2 = new ProjectTask("Повалять дурака", DateTime.Now.AddDays(10), teamLead, team[1]);
            ProjectTask task3 = new ProjectTask("Ответить маме на звонок", DateTime.Now.AddDays(8), teamLead, team[2]);
            ProjectTask task4 = new ProjectTask("Притвориться голубем", DateTime.Now.AddDays(9), teamLead, team[3]);
            ProjectTask task5 = new ProjectTask("Посмотреть сериальчик", DateTime.Now.AddDays(12), teamLead, team[4]);
            ProjectTask task6 = new ProjectTask("Позвать зубную фею", DateTime.Now.AddDays(6), teamLead, team[5]);
            ProjectTask task7 = new ProjectTask("Соблюдать шаббат", DateTime.Now.AddDays(13), teamLead, team[6]);
            ProjectTask task8 = new ProjectTask("Разговаривать по душам с тимлидом", DateTime.Now.AddDays(14), teamLead, team[7]);
            ProjectTask task9 = new ProjectTask("Работать за всех остальных", DateTime.Now.AddDays(7), teamLead, team[8]);
            ProjectTask task10 = new ProjectTask("Скучать, но еще работать", DateTime.Now.AddDays(5), teamLead, team[9]);

            ProjectTask taskRejectedAndReassigned = new ProjectTask("Ничего неделание", DateTime.Now.AddDays(11), teamLead, team[1]);
            ProjectTask taskRejectedAndDeleted = new ProjectTask("Обсуждение GTA VI", DateTime.Now.AddDays(10), teamLead, team[2]);

            ProjectTask[] allInitialTasks =
            {
                task1, task2, task3, task4, task5,
                task6, task7, task8, task9, task10,
                taskRejectedAndReassigned, taskRejectedAndDeleted
            };

            foreach (ProjectTask t in allInitialTasks)
            {
                project.AddTask(teamLead, t);
            }

            Console.WriteLine();
            project.StartExecution();

            ProjectTask forbiddenTask = new ProjectTask("Лишняя задача", DateTime.Now.AddDays(5), teamLead, team[1]);
            project.AddTask(teamLead, forbiddenTask);

            Console.WriteLine($"1. {task5.Executor.Name} передал задачу \"{task5.Description}\" коллеге ({team[3].Name}).");
            task5.Delegate(team[3]);
            task5.TakeInWork();
            task5.SendReport("Интеграция с сервером выполнена.", task5.Executor);
            task5.ReportReview(true);
            Console.WriteLine($"   Задача выполнена.\n");

            Console.WriteLine($"2. {taskRejectedAndReassigned.Executor.Name} отказался от задачи \"{taskRejectedAndReassigned.Description}\".");
            taskRejectedAndReassigned.Reject();
            Console.WriteLine($"   Тимлид назначил её на другого разработчика ({team[2].Name}).");
            taskRejectedAndReassigned.Assign(team[2]);
            taskRejectedAndReassigned.TakeInWork();
            taskRejectedAndReassigned.SendReport("Запросы к базе данных оптимизированы.", taskRejectedAndReassigned.Executor);
            taskRejectedAndReassigned.ReportReview(true);
            Console.WriteLine($"   Задача выполнена.\n");

            Console.WriteLine($"3. {taskRejectedAndDeleted.Executor.Name} отказался от задачи \"{taskRejectedAndDeleted.Description}\".");
            taskRejectedAndDeleted.Reject();
            project.DeleteTask(teamLead, taskRejectedAndDeleted);
            Console.WriteLine();

            Console.WriteLine($"4. {task2.Executor.Name} взял в работу задачу \"{task2.Description}\" и отправил первый отчёт.");
            task2.TakeInWork();
            task2.SendReport("Сделана базовая авторизация.", task2.Executor);
            Console.WriteLine($"   Тимлид проверил отчёт и вернул задачу на доработку.");
            task2.ReportReview(false);
            Console.WriteLine($"   Разработчик исправил замечания и сдал отчёт повторно.");
            task2.SendReport("Авторизация доработана и протестирована.", task2.Executor);
            task2.ReportReview(true);
            Console.WriteLine($"   Теперь отчёт принят, задача выполнена.\n");

            Console.WriteLine("5. Остальные участники команды завершают свои задачи...");
            ProjectTask[] remainingTasks = { task1, task3, task4, task6, task7, task8, task9, task10 };
            foreach (ProjectTask t in remainingTasks)
            {
                t.TakeInWork();
                t.SendReport($"Работа по задаче \"{t.Description}\" завершена.", t.Executor);
                t.ReportReview(true);
            }
            Console.WriteLine("   Все оставшиеся задачи проверены и приняты.\n");

            project.CloseProject();
            Console.WriteLine($"Итоговый статус проекта: {project.Status}.");

            Console.ReadKey();
        }
    }
}