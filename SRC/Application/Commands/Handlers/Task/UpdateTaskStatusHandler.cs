using MediatR;
using TaskManager.Domain.Exceptions;
using TaskManager.Domain.Interfaces;

namespace TaskManager.Application.Commands.Task;

public class UpdateTaskStatusHandler(ITaskRepository taskRepository, IProjectRepository projectRepository)
    : IRequestHandler<UpdateTaskStatusCommand, Unit>
{
    public async Task<Unit> Handle(UpdateTaskStatusCommand request, CancellationToken cancellationToken)
    {
        var project = await projectRepository.GetByIdAsync(request.ProjectId, request.OwnerId, cancellationToken);
        if (project == null)
            throw new NotFoundException("Project", request.ProjectId);

        var task = await taskRepository.GetByIdAsync(request.TaskId, request.ProjectId, cancellationToken);
        if (task == null)
            throw new NotFoundException("Task", request.TaskId);

        task.Status = request.StatusData.Status;
        task.UpdatedAt = DateTime.UtcNow;

        await taskRepository.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
