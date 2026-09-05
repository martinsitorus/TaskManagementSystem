public class TaskService : ITaskService
{
    private readonly ITaskRepository _taskRepository;
    private readonly IUserRepository _userRepository;
    private readonly ILogger<TaskService> _logger;

    public TaskService(
        ITaskRepository taskRepository,
        IUserRepository userRepository,
        ILogger<TaskService> logger)
    {
        _taskRepository = taskRepository;
        _userRepository = userRepository;
        _logger = logger;
    }

    public async Task<TaskItem> CreateTask(string title, string description, User? assignedTo, string priority, DateOnly dueDate)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new ArgumentException("Title is required.");
        }
        if (string.IsNullOrWhiteSpace(description))
        {
            throw new ArgumentException("Description is required.");
        }
        if (assignedTo == null)
        {
            throw new ArgumentException("Assigned user is required.");
        }

        await EnsureUserExists(assignedTo);

        var task = new TaskItem(title, description, assignedTo, priority, dueDate);
        await _taskRepository.AddTaskAsync(task);
        _logger.LogInformation("Created task '{Title}' (id {TaskId}) assigned to '{Username}'.", task.Title, task.Id, assignedTo.Username);
        return task;
    }

    public async Task<IEnumerable<TaskItem>> GetAllTasks()
    {
        return await _taskRepository.GetAllTasksAsync();
    }

    public async Task<TaskItem?> GetTaskById(string taskId)
    {
        return await _taskRepository.GetTaskByIdAsync(taskId);
    }

    public async Task<TaskItem?> GetTaskByTitle(string taskTitle)
    {
        return await _taskRepository.GetTaskByTitleAsync(taskTitle);
    }

    public async Task<IEnumerable<TaskItem>> GetTasksByUsername(string username)
    {
        var tasks = await _taskRepository.GetAllTasksAsync();
        return tasks.Where(t =>
            t.AssignedTo != null &&
            string.Equals(t.AssignedTo.Username, username, StringComparison.OrdinalIgnoreCase));
    }

    public async Task UpdateTask(string taskTitle, string newStatus, string newPriority, DateOnly newDueDate, User? newAssignedTo)
    {
        var task = await _taskRepository.GetTaskByTitleAsync(taskTitle);
        if (task == null)
        {
            _logger.LogWarning("Update failed: task '{Title}' not found.", taskTitle);
            throw new KeyNotFoundException($"Task '{taskTitle}' not found.");
        }

        if (newAssignedTo != null)
        {
            await EnsureUserExists(newAssignedTo);
        }

        task.UpdateStatus(newStatus);
        task.UpdatePriority(newPriority);
        task.UpdateDueDate(newDueDate);
        task.UpdateAssignedTo(newAssignedTo);

        await _taskRepository.UpdateTaskAsync(task);
        _logger.LogInformation("Updated task '{Title}' (id {TaskId}).", task.Title, task.Id);
    }

    public async Task<bool> DeleteTask(string taskTitle)
    {
        var task = await _taskRepository.GetTaskByTitleAsync(taskTitle);
        if (task == null)
        {
            _logger.LogWarning("Delete failed: task '{Title}' not found.", taskTitle);
            return false;
        }

        await _taskRepository.DeleteTaskAsync(task.Id);
        _logger.LogInformation("Deleted task '{Title}' (id {TaskId}).", task.Title, task.Id);
        return true;
    }

    private async Task EnsureUserExists(User user)
    {
        var exists = await _userRepository.UsernameExists(user.Username)
            || await _userRepository.UserIDExists(user.UserID);
        if (!exists)
        {
            _logger.LogWarning("Assigned user '{Username}' does not exist.", user.Username);
            throw new ArgumentException($"Assigned user '{user.Username}' does not exist.");
        }
    }
}
