using FluentValidation;
using TaskManager.Application.DTOs.Project;

namespace TaskManager.Application.Validators.Project;

public class UpdateProjectDTOValidator : AbstractValidator<UpdateProjectDTO>
{
    public UpdateProjectDTOValidator()
    {
        RuleFor(p => p.Name)
            .NotEmpty().WithMessage("Project name is required.")
            .MaximumLength(100).WithMessage("Project name must not exceed 100 characters.");

        RuleFor(p => p.Description)
            .MaximumLength(500).WithMessage("Description must not exceed 500 characters.");
    }
}
