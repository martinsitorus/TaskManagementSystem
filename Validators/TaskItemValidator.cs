using FluentValidation;
using TaskManagementSystem.Models;

public class TaskItemValidator : AbstractValidator<TaskItem>
{
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
            .NotEmpty().WithMessage("Priority is required.");

        RuleFor(t => t.AssignedTo)
            .NotNull().WithMessage("Assigned user is required.");
    }
}