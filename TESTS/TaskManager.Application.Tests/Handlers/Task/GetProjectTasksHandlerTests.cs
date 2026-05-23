using FluentAssertions;
using MapsterMapper;
using Moq;
using TaskManager.Application.DTOs.Task;
using TaskManager.Application.Queries.Task;
using TaskManager.Domain.Exceptions;
using TaskManager.Domain.Interfaces;
using Xunit;

namespace TaskManager.Application.Tests.Handlers.TaskItem;

public class GetProjectTasksHandlerTests
{
    private readonly Mock<ITaskRepository> _taskRepositoryMock;
    private readonly Mock<IProjectRepository> _projectRepositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly GetProjectTasksHandler _handler;

    public GetProjectTasksHandlerTests()
    {
        _taskRepositoryMock = new Mock<ITaskRepository>();
        _projectRepositoryMock = new Mock<IProjectRepository>();
        _mapperMock = new Mock<IMapper>();

        _handler = new GetProjectTasksHandler(_taskRepositoryMock.Object, _projectRepositoryMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task Handle_WhenProjectNotFound_ShouldThrowNotFoundException()
    {
        // Arrange
        var query = new GetProjectTasksQuery(Guid.NewGuid(), Guid.NewGuid());
        _projectRepositoryMock.Setup(x => x.GetByIdAsync(query.ProjectId, query.OwnerId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((global::Project?)null);

        // Act & Assert
        var action = async () => await _handler.Handle(query, CancellationToken.None);
        await action.Should().ThrowAsync<NotFoundException>().WithMessage("*Project*");
    }

    [Fact]
    public async Task Handle_WhenValid_ShouldReturnMappedTasks()
    {
        // Arrange
        var query = new GetProjectTasksQuery(Guid.NewGuid(), Guid.NewGuid());
        var tasks = new List<ProjectTask> { new ProjectTask(), new ProjectTask() };
        var dtos = new List<TaskDTO> { new TaskDTO(), new TaskDTO() };

        _projectRepositoryMock.Setup(x => x.GetByIdAsync(query.ProjectId, query.OwnerId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new global::Project());

        _taskRepositoryMock.Setup(x => x.GetAllByProjectIdAsync(query.ProjectId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(tasks);

        _mapperMock.Setup(x => x.Map<IEnumerable<TaskDTO>>(tasks)).Returns(dtos);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(dtos);
        _taskRepositoryMock.Verify(x => x.GetAllByProjectIdAsync(query.ProjectId, It.IsAny<CancellationToken>()), Times.Once);
    }
}
