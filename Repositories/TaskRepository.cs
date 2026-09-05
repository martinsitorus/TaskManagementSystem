using System.Text.Json;

public class TaskRepository : ITaskRepository
{
    private readonly string _filePath;
    private readonly List<TaskItem> _tasks;

    public TaskRepository(string? filePath = null)
    {
        _filePath = filePath ?? "./Data/tasks.json";
        _tasks = LoadTasksFromFile();
    }

    private List<TaskItem> LoadTasksFromFile()
    {
        if (!File.Exists(_filePath))
        {
            return new List<TaskItem>();
        }

        try
        {
            var json = File.ReadAllText(_filePath);
            var tasks = JsonSerializer.Deserialize<List<TaskItem>>(json) ?? new List<TaskItem>();
            // Backfill ids for entries persisted before ids were introduced.
            foreach (var task in tasks.Where(t => string.IsNullOrEmpty(t.Id)))
            {
                task.Id = Guid.NewGuid().ToString();
            }
            return tasks;
        }
        catch (JsonException)
        {
            // Corrupt seed/data file: start empty rather than crashing the app.
            return new List<TaskItem>();
        }
    }

    private void SaveTasksToFile()
    {
        var directory = Path.GetDirectoryName(_filePath);
        if (!string.IsNullOrEmpty(directory))
        {
            Directory.CreateDirectory(directory);
        }
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

    public async Task<TaskItem?> GetTaskByTitleAsync(string title)
    {
        return await Task.FromResult(
            _tasks.FirstOrDefault(t => t.Title.Equals(title, StringComparison.OrdinalIgnoreCase)));
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
