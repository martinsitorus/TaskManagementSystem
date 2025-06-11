using System.Text.Json;

public class TaskRepository : ITaskRepository
{
    private readonly string _filePath = "./Data/tasks.json";
    private List<TaskItem> _tasks;

    public TaskRepository()
    {
        _tasks = LoadTasksFromFile();
    }

    private List<TaskItem> LoadTasksFromFile()
    {
        if (!File.Exists(_filePath))
        {
            return new List<TaskItem>();
        }

        var json = File.ReadAllText(_filePath);
        return JsonSerializer.Deserialize<List<TaskItem>>(json) ?? new List<TaskItem>();
    }

    private void SaveTasksToFile()
    {
        var json = JsonSerializer.Serialize(_tasks);
        File.WriteAllText(_filePath, json);
    }

    public async Task<IEnumerable<TaskItem>> GetAllTasksAsync()
    {
        return await Task.FromResult(_tasks);
    }

    public async Task<TaskItem?> GetTaskByIdAsync(string taskId)
    {
        return await Task.FromResult(_tasks.FirstOrDefault(t => t.Id == taskId));
    }

    public async Task AddTaskAsync(TaskItem task)
    {
        _tasks.Add(task);
        SaveTasksToFile();
        await Task.CompletedTask;
    }

    public async Task UpdateTaskAsync(TaskItem task)
    {
        var existingTask = _tasks.FirstOrDefault(t => t.Id == task.Id);
        if (existingTask != null)
        {
            _tasks.Remove(existingTask);
            _tasks.Add(task);
            SaveTasksToFile();
        }
        await Task.CompletedTask;
    }

    public async Task DeleteTaskAsync(string taskId)
    {
        var task = _tasks.FirstOrDefault(t => t.Id == taskId);
        if (task != null)
        {
            _tasks.Remove(task);
            SaveTasksToFile();
        }
        await Task.CompletedTask;
    }
}