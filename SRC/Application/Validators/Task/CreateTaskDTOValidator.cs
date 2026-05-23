using FluentValidation;
using TaskManager.Application.DTOs.Task;

namespace TaskManager.Application.Validators.Task;

public class CreateTaskDTOValidator : AbstractValidator<CreateTaskDTO>
{
    public CreateTaskDTOValidator()
    {
        RuleFor(t => t.Title)
            .NotEmpty().WithMessage("Task title is required.")
            .MaximumLength(100).WithMessage("Task title must not exceed 100 characters.");

        RuleFor(t => t.Description)
            .MaximumLength(1000).WithMessage("Description must not exceed 1000 characters.");
            
        RuleFor(t => t.Priority)
            .IsInEnum().WithMessage("Invalid priority level.");
            
        RuleFor(t => t.Status)
            .IsInEnum().WithMessage("Invalid status level.");
    }
}
