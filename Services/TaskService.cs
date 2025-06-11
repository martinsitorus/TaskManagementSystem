using System.Text.Json;
public class TaskService : ITaskService
{
    private readonly ITaskRepository _taskRepository;

    public TaskService(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public async Task<TaskItem> CreateTask(string title, string description, User assignedTo, string priority, DateOnly dueDate)
    {
        var task = new TaskItem(title, description, assignedTo, priority, dueDate);
        await _taskRepository.AddTaskAsync(task);
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

    public async Task UpdateTask(string taskId, string newStatus, string newPriority, DateOnly newDueDate, User newAssignedTo)
    {
        var task = await _taskRepository.GetTaskByIdAsync(taskId);
        if (task == null)
        {
            throw new Exception("Task not found");
        }

        task.UpdateStatus(newStatus);
        task.UpdatePriority(newPriority);
        task.UpdateDueDate(newDueDate);
        task.UpdateAssignedTo(newAssignedTo);

        await _taskRepository.UpdateTaskAsync(task);
    }

    public async Task DeleteTask(string taskId)
    {
        await _taskRepository.DeleteTaskAsync(taskId);
    }
}