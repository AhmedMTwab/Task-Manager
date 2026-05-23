using TaskManager.Application.DTOs.Project;

namespace TaskManager.Application.Commands.Project;

public record CreateProjectCommand(CreateProjectDTO ProjectData, Guid OwnerId) : ICommand;
