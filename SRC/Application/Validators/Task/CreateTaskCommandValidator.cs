using FluentValidation;
using TaskManager.Application.Commands.Task;

namespace TaskManager.Application.Validators.Task;

public class CreateTaskCommandValidator : AbstractValidator<CreateTaskCommand>
{
    public CreateTaskCommandValidator()
    {
        RuleFor(t => t.TaskData.Title)
            .NotEmpty().WithMessage("Task title is required.")
            .MaximumLength(100).WithMessage("Task title must not exceed 100 characters.");

        RuleFor(t => t.TaskData.Description)
            .MaximumLength(1000).WithMessage("Description must not exceed 1000 characters.");
            
        RuleFor(t => t.TaskData.Priority)
            .IsInEnum().WithMessage("Invalid priority level.");
            
        RuleFor(t => t.TaskData.Status)
            .IsInEnum().WithMessage("Invalid status level.");
    }
}
