public interface ITaskService
{
    // Create a new task
    Task<TaskItem> CreateTask(string title, string description, User? assignedTo, string priority, DateOnly dueDate);

    // Get all tasks
    Task<IEnumerable<TaskItem>> GetAllTasks();

    // Get a task by its ID
    Task<TaskItem?> GetTaskById(string taskId);

    // Get a task by its title (titles are the public lookup key used by the API routes)
    Task<TaskItem?> GetTaskByTitle(string taskTitle);

    // Get tasks assigned to a specific username
    Task<IEnumerable<TaskItem>> GetTasksByUsername(string username);

    // Update a task (looked up by title)
    Task UpdateTask(string taskTitle, string newStatus, string newPriority, DateOnly newDueDate, User? newAssignedTo);

    // Delete a task (looked up by title); returns false when the task does not exist
    Task<bool> DeleteTask(string taskTitle);
}
