using MapsterMapper;
using MediatR;
using TaskManager.Domain.Exceptions;
using TaskManager.Domain.Interfaces;

namespace TaskManager.Application.Commands.Task;

public class CreateTaskHandler(ITaskRepository taskRepository, IProjectRepository projectRepository, IMapper mapper) 
    : IRequestHandler<CreateTaskCommand, Unit>
{
    public async Task<Unit> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
    {
        var project = await projectRepository.GetByIdAsync(request.ProjectId, request.OwnerId, cancellationToken);
        if (project == null)
        {
            throw new NotFoundException("Project", request.ProjectId);
        }

        var task = mapper.Map<ProjectTask>(request.TaskData);
        task.ProjectId = request.ProjectId;
        
        await taskRepository.AddAsync(task, cancellationToken);
        await taskRepository.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
