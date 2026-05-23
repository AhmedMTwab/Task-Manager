namespace TaskManager.Application.Commands.Task;

public record DeleteTaskCommand(Guid TaskId, Guid ProjectId, Guid OwnerId) : ICommand;
