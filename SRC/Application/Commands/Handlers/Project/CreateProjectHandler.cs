using Mapster;
using MediatR;
using TaskManager.Domain.Interfaces;

namespace TaskManager.Application.Commands.Project;

public class CreateProjectHandler(IProjectRepository projectRepository) : IRequestHandler<CreateProjectCommand, Unit>
{
    public async Task<Unit> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
    {
        var project = request.ProjectData.Adapt<global::Project>();
        project.OwnerId = request.OwnerId;

        await projectRepository.AddAsync(project, cancellationToken);
        await projectRepository.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
