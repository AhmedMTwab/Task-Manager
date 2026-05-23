using TaskManager.Application.DTOs.Project;

namespace TaskManager.Application.Queries.Project;

public record GetProjectByIdQuery(Guid Id, Guid OwnerId) : IQuery<ProjectDTO>;
