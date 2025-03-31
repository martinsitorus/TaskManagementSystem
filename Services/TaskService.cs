public class TaskService : ITaskService
{
    public TaskItem CreateTask(string title, string description, string assignedTo, string priority, DateOnly dueDate)
    {
        return new TaskItem(title, description, assignedTo, priority, dueDate);
    }

    public object GetAllTasks()
    {
        return new List<TaskItem>();
    }

    public void UpdateTaskStatus(TaskItem task, string newStatus)
    {
        task.UpdateStatus(newStatus);
    }
}