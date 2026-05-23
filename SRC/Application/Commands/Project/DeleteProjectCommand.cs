namespace TaskManager.Application.Commands.Project;

public record DeleteProjectCommand(Guid Id, Guid OwnerId) : ICommand;
