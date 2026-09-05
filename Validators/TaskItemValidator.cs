using FluentValidation;

public class TaskItemValidator : AbstractValidator<TaskItem>
{
    private static readonly string[] AllowedPriorities = ["Low", "Medium", "High"];
    private static readonly string[] AllowedStatuses = ["To Do", "In Progress", "Done"];

    public TaskItemValidator()
    {
        RuleFor(t => t.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(100);

        RuleFor(t => t.Description)
            .NotEmpty().WithMessage("Description is required.");

        RuleFor(t => t.DueDate)
            .Must(d => d >= DateOnly.FromDateTime(DateTime.Today))
            .WithMessage("Due date cannot be in the past.");

        RuleFor(t => t.Priority)
            .NotEmpty().WithMessage("Priority is required.")
            .Must(p => AllowedPriorities.Contains(p, StringComparer.OrdinalIgnoreCase))
            .WithMessage($"Priority must be one of: {string.Join(", ", AllowedPriorities)}.");

        RuleFor(t => t.Status)
            .Must(s => AllowedStatuses.Contains(s, StringComparer.OrdinalIgnoreCase))
            .WithMessage($"Status must be one of: {string.Join(", ", AllowedStatuses)}.");

        RuleFor(t => t.AssignedTo)
            .NotNull().WithMessage("Assigned user is required.");
    }
}
