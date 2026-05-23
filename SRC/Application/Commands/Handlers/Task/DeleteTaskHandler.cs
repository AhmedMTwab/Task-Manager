using MediatR;
using TaskManager.Domain.Exceptions;
using TaskManager.Domain.Interfaces;

namespace TaskManager.Application.Commands.Task;

public class DeleteTaskHandler(ITaskRepository taskRepository, IProjectRepository projectRepository) 
    : IRequestHandler<DeleteTaskCommand, Unit>
{
    public async Task<Unit> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
    {
        var project = await projectRepository.GetByIdAsync(request.ProjectId, request.OwnerId, cancellationToken);
        if (project == null)
            throw new NotFoundException("Project", request.ProjectId);

        var task = await taskRepository.GetByIdAsync(request.TaskId, request.ProjectId, cancellationToken);
        if (task == null)
            throw new NotFoundException("Task", request.TaskId);

        task.IsDeleted = true;
        task.UpdatedAt = DateTime.UtcNow;

        await taskRepository.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
