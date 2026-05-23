using FluentAssertions;
using Moq;
using TaskManager.Application.Commands.Task;
using TaskManager.Application.DTOs.Task;
using TaskManager.Domain.Exceptions;
using TaskManager.Domain.Interfaces;
using Xunit;

namespace TaskManager.Application.Tests.Handlers.TaskItem;

public class UpdateTaskStatusHandlerTests
{
    private readonly Mock<ITaskRepository> _taskRepositoryMock;
    private readonly Mock<IProjectRepository> _projectRepositoryMock;
    private readonly UpdateTaskStatusHandler _handler;

    public UpdateTaskStatusHandlerTests()
    {
        _taskRepositoryMock = new Mock<ITaskRepository>();
        _projectRepositoryMock = new Mock<IProjectRepository>();

        _handler = new UpdateTaskStatusHandler(_taskRepositoryMock.Object, _projectRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_WhenProjectNotFound_ShouldThrowNotFoundException()
    {
        // Arrange
        var command = new UpdateTaskStatusCommand(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), new UpdateTaskStatusDTO());
        _projectRepositoryMock.Setup(x => x.GetByIdAsync(command.ProjectId, command.OwnerId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((global::Project?)null);

        // Act & Assert
        var action = async () => await _handler.Handle(command, CancellationToken.None);
        await action.Should().ThrowAsync<NotFoundException>().WithMessage("*Project*");
    }

    [Fact]
    public async Task Handle_WhenTaskNotFound_ShouldThrowNotFoundException()
    {
        // Arrange
        var command = new UpdateTaskStatusCommand(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), new UpdateTaskStatusDTO());
        _projectRepositoryMock.Setup(x => x.GetByIdAsync(command.ProjectId, command.OwnerId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new global::Project());

        _taskRepositoryMock.Setup(x => x.GetByIdAsync(command.TaskId, command.ProjectId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((ProjectTask?)null);

        // Act & Assert
        var action = async () => await _handler.Handle(command, CancellationToken.None);
        await action.Should().ThrowAsync<NotFoundException>().WithMessage("*Task*");
    }

    [Fact]
    public async Task Handle_WhenValid_ShouldUpdateStatusAndSave()
    {
        // Arrange
        var command = new UpdateTaskStatusCommand(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), new UpdateTaskStatusDTO { Status = StatusEnum.Completed });
        var task = new ProjectTask { Id = command.TaskId, Status = StatusEnum.Pending };

        _projectRepositoryMock.Setup(x => x.GetByIdAsync(command.ProjectId, command.OwnerId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new global::Project());

        _taskRepositoryMock.Setup(x => x.GetByIdAsync(command.TaskId, command.ProjectId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(task);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().Be(MediatR.Unit.Value);
        task.Status.Should().Be(StatusEnum.Completed);
        task.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));

        _taskRepositoryMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}
