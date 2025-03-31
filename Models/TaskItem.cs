public class TaskItem(string title, string description, User assignedTo, string priority, DateOnly dueDate)
{
    public string Title { get; set; } = title;
    public string Description { get; set; } = description;
    public DateOnly DueDate { get; set; } = dueDate;
    public string Priority { get; set; } = priority;
    public string Status { get; set; } = "To Do";
    public User AssignedTo { get; set; } = assignedTo;
    public void UpdateStatus(string newStatus)
    {
        Status = newStatus;
    }
}