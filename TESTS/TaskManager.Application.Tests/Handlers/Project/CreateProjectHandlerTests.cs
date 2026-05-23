using FluentAssertions;
using MapsterMapper;
using Moq;
using TaskManager.Application.Commands.Project;
using TaskManager.Application.DTOs.Project;
using TaskManager.Domain.Interfaces;
using Xunit;

namespace TaskManager.Application.Tests.Handlers.Project;

public class CreateProjectHandlerTests
{
    private readonly Mock<IProjectRepository> _projectRepositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly CreateProjectHandler _handler;

    public CreateProjectHandlerTests()
    {
        _projectRepositoryMock = new Mock<IProjectRepository>();
        _mapperMock = new Mock<IMapper>();
        _handler = new CreateProjectHandler(_projectRepositoryMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task Handle_Should_Add_Project_And_Save()
    {
        // Arrange
        var ownerId = Guid.NewGuid();
        var dto = new CreateProjectDTO { Name = "Test Project", Description = "Test Description" };
        var command = new CreateProjectCommand(dto, ownerId);

        var mappedProject = new global::Project { Id = Guid.NewGuid(), Name = dto.Name, Description = dto.Description };

        _mapperMock.Setup(m => m.Map<global::Project>(dto)).Returns(mappedProject);
        _projectRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<global::Project>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);
        _projectRepositoryMock.Setup(repo => repo.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().Be(MediatR.Unit.Value);

        mappedProject.OwnerId.Should().Be(ownerId);

        _projectRepositoryMock.Verify(repo => repo.AddAsync(It.Is<global::Project>(p => p.Id == mappedProject.Id && p.OwnerId == ownerId), It.IsAny<CancellationToken>()), Times.Once);
        _projectRepositoryMock.Verify(repo => repo.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}
