using TaskManager.Application.DTOs.Task;

namespace TaskManager.Application.Commands.Task;

public record UpdateTaskStatusCommand(Guid TaskId, Guid ProjectId, Guid OwnerId, UpdateTaskStatusDTO StatusData) : ICommand;
