public interface ITaskService
{
    // Create a new task
    Task<TaskItem> CreateTask(string title, string description, User assignedTo, string priority, DateOnly dueDate);

    // Get all tasks
    Task<IEnumerable<TaskItem>> GetAllTasks();

    // Get a task by its ID
    Task<TaskItem?> GetTaskById(string taskId);

    // Update a task
    Task UpdateTask(string taskId, string newStatus, string newPriority, DateOnly newDueDate, User newAssignedTo);

    // Delete a task
    Task DeleteTask(string taskId);
}