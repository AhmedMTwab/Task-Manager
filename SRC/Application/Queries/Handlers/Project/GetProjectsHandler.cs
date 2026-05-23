using Mapster;
using MediatR;
using TaskManager.Application.DTOs.Project;
using TaskManager.Domain.Interfaces;

namespace TaskManager.Application.Queries.Project;

public class GetProjectsHandler(IProjectRepository projectRepository) 
    : IRequestHandler<GetProjectsQuery, IEnumerable<ProjectDTO>>
{
    public async Task<IEnumerable<ProjectDTO>> Handle(GetProjectsQuery request, CancellationToken cancellationToken)
    {
        var projects = await projectRepository.GetAllByOwnerIdAsync(request.OwnerId, cancellationToken);
        return projects.Adapt<IEnumerable<ProjectDTO>>();
    }
}
