using FluentValidation;

public class UserValidator : AbstractValidator<User>
{
    public UserValidator()
    {
        RuleFor(u => u.Username)
            .NotEmpty().WithMessage("Username is required.")
            .MaximumLength(50);

        RuleFor(u => u.UserID)
            .NotEmpty().WithMessage("UserID is required.")
            .MaximumLength(50);
    }
}