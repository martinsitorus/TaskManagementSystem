public interface ITaskService
{
    TaskItem CreateTask(string title, string description, string assignedTo, string priority, DateOnly dueDate);
    object GetAllTasks();
    void UpdateTaskStatus(TaskItem task, string newStatus);
}