using MapsterMapper;
using MediatR;
using TaskManager.Application.DTOs.Task;
using TaskManager.Domain.Exceptions;
using TaskManager.Domain.Interfaces;

namespace TaskManager.Application.Queries.Task;

public class GetProjectTasksHandler(ITaskRepository taskRepository, IProjectRepository projectRepository, IMapper mapper)
    : IRequestHandler<GetProjectTasksQuery, IEnumerable<TaskDTO>>
{
    public async Task<IEnumerable<TaskDTO>> Handle(GetProjectTasksQuery request, CancellationToken cancellationToken)
    {
        var project = await projectRepository.GetByIdAsync(request.ProjectId, request.OwnerId, cancellationToken);
        if (project == null)
            throw new NotFoundException("Project", request.ProjectId);

        var tasks = await taskRepository.GetAllByProjectIdAsync(request.ProjectId, cancellationToken);

        return mapper.Map<IEnumerable<TaskDTO>>(tasks);
    }
}
