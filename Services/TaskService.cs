using System.Text.Json;
public class TaskService : ITaskService
{
    private readonly string _filePath = "./Data/tasks.json";
    private readonly List<TaskItem> _tasks = new List<TaskItem>();

    public TaskService()
    {
        // Load tasks from JSON file at startup
        if (File.Exists(_filePath))
        {
            var json = File.ReadAllText(_filePath);
            var tasks = JsonSerializer.Deserialize<List<TaskItem>>(json) ?? new List<TaskItem>();
            _tasks.AddRange(tasks); // Add deserialized tasks to the existing list
        }
        else
        {
            _tasks = new List<TaskItem>();
        }
    }
    public void SaveTasksToFile()
    {
        var json = JsonSerializer.Serialize(_tasks);
        File.WriteAllText(_filePath, json);
    }
    public void LoadTasksFromFile()
    {
        if (File.Exists(_filePath))
        {
            var json = File.ReadAllText(_filePath);
            var tasks = JsonSerializer.Deserialize<List<TaskItem>>(json) ?? new List<TaskItem>();
            _tasks.Clear(); // Clear the existing list
            _tasks.AddRange(tasks); // Add deserialized tasks to the list
        }
        else
        {
            _tasks.Clear(); // Ensure the list is empty if the file doesn't exist
        }
    }

    public TaskItem CreateTask(string title, string description, User assignedTo, string priority, DateOnly dueDate)
    {
        var task = new TaskItem(title, description, assignedTo, priority, dueDate);
        _tasks.Add(task);
        SaveTasksToFile();
        return new TaskItem(title, description, assignedTo, priority, dueDate);
    }

    public IEnumerable<TaskItem> GetAllTasks()
    {
        LoadTasksFromFile();
        return _tasks;
    }

    public void UpdateTaskStatus(TaskItem task, string newStatus)
    {
        LoadTasksFromFile();
        task.UpdateStatus(newStatus);
    }
    public void UpdateTaskPriority(TaskItem task, string newPriority)
    {
        LoadTasksFromFile();
        task.UpdatePriority(newPriority);
    }
    public void UpdateTaskDueDate(TaskItem task, DateOnly newDueDate)
    {
        LoadTasksFromFile();
        task.UpdateDueDate(newDueDate);
    }
    public void UpdateTaskAssignedTo(TaskItem task, User newAssignedTo)
    {
        LoadTasksFromFile();
        task.UpdateAssignedTo(newAssignedTo);
    }
    public void UpdateTask(string taskId, string newStatus, string newPriority, DateOnly newDueDate, User newAssignedTo)
    {
        LoadTasksFromFile();
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
        LoadTasksFromFile();
        var task = GetAllTasks().FirstOrDefault(t => t.Title == taskId);
        if (task == null)
        {
            throw new Exception("Task not found");
        }
        _tasks.Remove(task);
        SaveTasksToFile();
    }
}