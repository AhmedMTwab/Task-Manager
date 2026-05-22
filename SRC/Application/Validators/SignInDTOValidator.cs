using FluentValidation;
using TaskManager.Application.DTOs.Authentication;

namespace TaskManager.Application.Validators.Authentication;

public class SignInDTOValidator : AbstractValidator<SignInDTO>
{
    public SignInDTOValidator()
    {
        RuleFor(s => s.Username).Length(5, 15);
    }
}
