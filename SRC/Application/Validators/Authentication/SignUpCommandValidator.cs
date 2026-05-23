using FluentValidation;
using TaskManager.Application.Commands.Authentication;

namespace TaskManager.Application.Validators.Authentication;

public class SignUpCommandValidator : AbstractValidator<SignUpCommand>
{
    public SignUpCommandValidator()
    {
        RuleFor(s => s.SignUpData.Username).Length(5, 15);
        RuleFor(s => s.SignUpData.Email).EmailAddress();
        RuleFor(s => s.SignUpData.Password).Length(8, 20);
        RuleFor(s => s.SignUpData.ConfirmPassword).Equal(s => s.SignUpData.Password).WithMessage("Passwords do not match.");
    }
}
