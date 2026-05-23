using TaskManager.Application.DTOs.Task;

namespace TaskManager.Application.Commands.Task;

public record CreateTaskCommand(Guid ProjectId, CreateTaskDTO TaskData, Guid OwnerId) : ICommand;
