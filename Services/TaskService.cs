public class TaskService : ITaskService
{
    private readonly List<TaskItem> _tasks = new List<TaskItem>();
    public TaskItem CreateTask(string title, string description, User assignedTo, string priority, DateOnly dueDate)
    {
        var task = new TaskItem(title, description, assignedTo, priority, dueDate);
        _tasks.Add(task);
        
        return new TaskItem(title, description, assignedTo, priority, dueDate);
    }

    public IEnumerable<TaskItem> GetAllTasks()
    {
        return _tasks;
    }

    public void UpdateTaskStatus(TaskItem task, string newStatus)
    {
        task.UpdateStatus(newStatus);
    }
    public void UpdateTaskPriority(TaskItem task, string newPriority)
    {
        task.UpdatePriority(newPriority);
    }
    public void UpdateTaskDueDate(TaskItem task, DateOnly newDueDate)
    {
        task.UpdateDueDate(newDueDate);
    }
    public void UpdateTaskAssignedTo(TaskItem task, User newAssignedTo)
    {
        task.UpdateAssignedTo(newAssignedTo);
    }
    public void UpdateTask(string taskId, string newStatus, string newPriority, DateOnly newDueDate, User newAssignedTo)
    {
        var task = GetAllTasks().FirstOrDefault(t => t.Title == taskId);
        if (task == null)
        {
            throw new Exception("Task not found");
        }
        task.UpdateStatus(newStatus);
        task.UpdatePriority(newPriority);
        task.UpdateDueDate(newDueDate);
        task.UpdateAssignedTo(newAssignedTo);
    }
    public void DeleteTask(string taskId)
    {
        var task = GetAllTasks().FirstOrDefault(t => t.Title == taskId);
        if (task == null)
        {
            throw new Exception("Task not found");
        }
        // Logic to delete the task
    }
}