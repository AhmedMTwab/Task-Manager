using FluentAssertions;
using MapsterMapper;
using Moq;
using TaskManager.Application.Commands.Task;
using TaskManager.Application.DTOs.Task;
using TaskManager.Domain.Exceptions;
using TaskManager.Domain.Interfaces;
using Xunit;

namespace TaskManager.Application.Tests.Handlers.TaskItem;

public class CreateTaskHandlerTests
{
    private readonly Mock<ITaskRepository> _taskRepositoryMock;
    private readonly Mock<IProjectRepository> _projectRepositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly CreateTaskHandler _handler;

    public CreateTaskHandlerTests()
    {
        _taskRepositoryMock = new Mock<ITaskRepository>();
        _projectRepositoryMock = new Mock<IProjectRepository>();
        _mapperMock = new Mock<IMapper>();

        _handler = new CreateTaskHandler(_taskRepositoryMock.Object, _projectRepositoryMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task Handle_WhenProjectDoesNotExist_ShouldThrowNotFoundException()
    {
        // Arrange
        var command = new CreateTaskCommand(Guid.NewGuid(), new CreateTaskDTO(), Guid.NewGuid());
        _projectRepositoryMock.Setup(x => x.GetByIdAsync(command.ProjectId, command.OwnerId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((global::Project?)null);

        // Act & Assert
        var action = async () => await _handler.Handle(command, CancellationToken.None);
        await action.Should().ThrowAsync<NotFoundException>().WithMessage("*Project*");

        _taskRepositoryMock.Verify(x => x.AddAsync(It.IsAny<ProjectTask>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task Handle_WhenValid_ShouldAddTaskAndSave()
    {
        // Arrange
        var command = new CreateTaskCommand(Guid.NewGuid(), new CreateTaskDTO { Title = "Test Task" }, Guid.NewGuid());
        var project = new global::Project { Id = command.ProjectId, OwnerId = command.OwnerId };
        var mappedTask = new ProjectTask { Title = "Test Task" };

        _projectRepositoryMock.Setup(x => x.GetByIdAsync(command.ProjectId, command.OwnerId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(project);

        _mapperMock.Setup(x => x.Map<ProjectTask>(command.TaskData)).Returns(mappedTask);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().Be(MediatR.Unit.Value);
        mappedTask.ProjectId.Should().Be(command.ProjectId);

        _taskRepositoryMock.Verify(x => x.AddAsync(mappedTask, It.IsAny<CancellationToken>()), Times.Once);
        _taskRepositoryMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}
