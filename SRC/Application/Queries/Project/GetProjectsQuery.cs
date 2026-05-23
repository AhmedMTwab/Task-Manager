using TaskManager.Application.DTOs.Project;

namespace TaskManager.Application.Queries.Project;

public record GetProjectsQuery(Guid OwnerId) : IQuery<IEnumerable<ProjectDTO>>;
