using FluentValidation;
using TaskManager.Application.Commands.Project;

namespace TaskManager.Application.Validators.Project;

public class CreateProjectCommandValidator : AbstractValidator<CreateProjectCommand>
{
    public CreateProjectCommandValidator()
    {
        RuleFor(p => p.ProjectData.Name)
            .NotEmpty().WithMessage("Project name is required.")
            .MaximumLength(100).WithMessage("Project name must not exceed 100 characters.");

        RuleFor(p => p.ProjectData.Description)
            .MaximumLength(500).WithMessage("Description must not exceed 500 characters.");
    }
}
