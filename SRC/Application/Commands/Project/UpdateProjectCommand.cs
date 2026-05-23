using TaskManager.Application.DTOs.Project;

namespace TaskManager.Application.Commands.Project;

public record UpdateProjectCommand(Guid Id, UpdateProjectDTO ProjectData, Guid OwnerId) : ICommand;
