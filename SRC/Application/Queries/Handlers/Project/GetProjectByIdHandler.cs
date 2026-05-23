using MapsterMapper;
using MediatR;
using TaskManager.Application.DTOs.Project;
using TaskManager.Domain.Exceptions;
using TaskManager.Domain.Interfaces;

namespace TaskManager.Application.Queries.Project;

public class GetProjectByIdHandler(IProjectRepository projectRepository, IMapper mapper) 
    : IRequestHandler<GetProjectByIdQuery, ProjectDTO>
{
    public async Task<ProjectDTO> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
    {
        var project = await projectRepository.GetByIdAsync(request.Id, request.OwnerId, cancellationToken);
        if (project == null)
            throw new NotFoundException("Project", request.Id);

        return mapper.Map<ProjectDTO>(project);
    }
}
