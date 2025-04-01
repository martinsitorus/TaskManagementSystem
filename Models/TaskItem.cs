public class TaskItem
{
    public string Title { get; set; }
    public string Description { get; set; }
    private DateOnly _dueDate;

    public DateOnly DueDate 
    { 
        get => _dueDate; 
        set
        {
            if (value < DateOnly.FromDateTime(DateTime.Now))
            {
                throw new ArgumentException("Due date cannot be in the past.");
            }
            _dueDate = value;
        }
    }

    public string Priority { get; set; }
    public string Status { get; set; } = "To Do";
    public User AssignedTo { get; set; }

    // Constructor
    public TaskItem(string title, string description, User assignedTo, string priority, DateOnly dueDate)
    {
        Title = title;
        Description = description;
        AssignedTo = assignedTo;
        Priority = priority;
        DueDate = dueDate; // Use the property to ensure validation
    }

    public void UpdateStatus(string newStatus)
    {
        Status = newStatus;
    }

    public void UpdatePriority(string newPriority)
    {
        Priority = newPriority;
    }

    public void UpdateDueDate(DateOnly newDueDate)
    {
        if (newDueDate < DateOnly.FromDateTime(DateTime.Now))
        {
            throw new ArgumentException("Due date cannot be in the past.");
        }
        DueDate = newDueDate;
    }

    public void UpdateAssignedTo(User newAssignedTo)
    {
        AssignedTo = newAssignedTo;
    }
    
}