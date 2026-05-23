using MediatR;
using TaskManager.Domain.Exceptions;
using TaskManager.Domain.Interfaces;

namespace TaskManager.Application.Commands.Project;

public class DeleteProjectHandler(IProjectRepository projectRepository) : IRequestHandler<DeleteProjectCommand, Unit>
{
    public async Task<Unit> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
    {
        var project = await projectRepository.GetByIdWithTasksAsync(request.Id, request.OwnerId, cancellationToken);
        if (project == null)
            throw new NotFoundException("Project", request.Id);

        project.IsDeleted = true;
        project.UpdatedAt = DateTime.UtcNow;

        foreach (var task in project.Tasks)
        {
            task.IsDeleted = true;
            task.UpdatedAt = DateTime.UtcNow;
        }

        await projectRepository.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
