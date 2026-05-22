using FluentValidation;
using TaskManager.Application.DTOs.Authentication;

namespace TaskManager.Application.Validators.Authentication;

public class SignUpDTOValidator : AbstractValidator<SignUpDTO>
{
    public SignUpDTOValidator()
    {
        RuleFor(s => s.Username).Length(5, 15);
        RuleFor(s => s.Email).EmailAddress();
        RuleFor(s => s.Password).Length(8, 20);
        RuleFor(s => s.ConfirmPassword).Equal(s => s.Password).WithMessage("Passwords do not match.");
    }
}
