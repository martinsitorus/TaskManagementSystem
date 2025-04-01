public interface ITaskService
{
    TaskItem CreateTask(string title, string description, User assignedTo, string priority, DateOnly dueDate);
    IEnumerable<TaskItem> GetAllTasks();
    void UpdateTaskStatus(TaskItem task, string newStatus);
    void UpdateTaskPriority(TaskItem task, string newPriority);
    void UpdateTaskDueDate(TaskItem task, DateOnly newDueDate);
    void UpdateTaskAssignedTo(TaskItem task, User newAssignedTo);
    void UpdateTask(string taskId, string newStatus, string newPriority, DateOnly newDueDate, User newAssignedTo);
    void DeleteTask(string taskId);
}