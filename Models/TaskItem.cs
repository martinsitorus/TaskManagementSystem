public class TaskItem
{
    private static readonly string[] AllowedPriorities = ["Low", "Medium", "High"];
    private static readonly string[] AllowedStatuses = ["To Do", "In Progress", "Done"];

    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    private DateOnly _dueDate;

    public DateOnly DueDate
    {
        get => _dueDate;
        set
        {
            ValidateDueDate(value);
            _dueDate = value;
        }
    }

    private string _priority = "Medium";

    public string Priority
    {
        get => _priority;
        set
        {
            ValidatePriority(value);
            _priority = value;
        }
    }

    public string Status { get; set; } = "To Do";
    public User? AssignedTo { get; set; }

    // Constructor
    public TaskItem(string title, string description, User? assignedTo, string priority, DateOnly dueDate)
    {
        Title = title;
        Description = description;
        AssignedTo = assignedTo;
        Priority = priority; // Validated by the property setter
        DueDate = dueDate; // Validated by the property setter
    }

    public void UpdateStatus(string newStatus)
    {
        ValidateStatus(newStatus);
        Status = newStatus;
    }

    public void UpdatePriority(string newPriority)
    {
        Priority = newPriority; // Validated by the property setter
    }

    public void UpdateDueDate(DateOnly newDueDate)
    {
        DueDate = newDueDate; // Validated by the property setter
    }

    public void UpdateAssignedTo(User? newAssignedTo)
    {
        AssignedTo = newAssignedTo;
    }

    public static void ValidateDueDate(DateOnly value)
    {
        if (value < DateOnly.FromDateTime(DateTime.Now))
        {
            throw new ArgumentException("Due date cannot be in the past.");
        }
    }

    public static void ValidatePriority(string value)
    {
        if (!AllowedPriorities.Contains(value, StringComparer.OrdinalIgnoreCase))
        {
            throw new ArgumentException($"Priority must be one of: {string.Join(", ", AllowedPriorities)}.");
        }
    }

    public static void ValidateStatus(string value)
    {
        if (!AllowedStatuses.Contains(value, StringComparer.OrdinalIgnoreCase))
        {
            throw new ArgumentException($"Status must be one of: {string.Join(", ", AllowedStatuses)}.");
        }
    }
}
