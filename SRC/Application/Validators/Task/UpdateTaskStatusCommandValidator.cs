using FluentValidation;
using TaskManager.Application.Commands.Task;

namespace TaskManager.Application.Validators.Task;

public class UpdateTaskStatusCommandValidator : AbstractValidator<UpdateTaskStatusCommand>
{
    public UpdateTaskStatusCommandValidator()
    {
        RuleFor(t => t.StatusData.Status)
            .IsInEnum().WithMessage("Invalid status level.");
    }
}
