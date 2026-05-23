using FluentValidation;
using TaskManager.Application.Commands.Authentication;

namespace TaskManager.Application.Validators.Authentication;

public class SignInCommandValidator : AbstractValidator<SignInCommand>
{
    public SignInCommandValidator()
    {
        RuleFor(s => s.SignInData.Username).Length(5, 15);
    }
}
