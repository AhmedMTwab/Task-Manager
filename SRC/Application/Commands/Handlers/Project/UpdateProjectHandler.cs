using MediatR;
using TaskManager.Domain.Exceptions;
using TaskManager.Domain.Interfaces;

namespace TaskManager.Application.Commands.Project;

public class UpdateProjectHandler(IProjectRepository projectRepository) : IRequestHandler<UpdateProjectCommand, Unit>
{
    public async Task<Unit> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
    {
        var project = await projectRepository.GetByIdAsync(request.Id, request.OwnerId, cancellationToken);
        if (project == null)
            throw new NotFoundException("Project", request.Id);

        project.Name = request.ProjectData.Name;
        project.Description = request.ProjectData.Description;
        project.UpdatedAt = DateTime.UtcNow;

        await projectRepository.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
