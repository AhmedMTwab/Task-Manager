using TaskManager.Application.DTOs.Task;

namespace TaskManager.Application.Queries.Task;

public record GetProjectTasksQuery(Guid ProjectId, Guid OwnerId) : IQuery<IEnumerable<TaskDTO>>;
