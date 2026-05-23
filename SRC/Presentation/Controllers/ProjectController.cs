using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.Commands.Project;
using TaskManager.Application.Queries.Project;
using TaskManager.Application.DTOs;
using TaskManager.Application.DTOs.Project;

namespace TaskManager.Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ProjectController(ISender mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create(CreateProjectDTO createProjectDTO)
    {
        var ownerId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        await mediator.Send(new CreateProjectCommand(createProjectDTO, ownerId));
        return Ok(ApiResponse.Success(201, "Project created successfully."));
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var ownerId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var projects = await mediator.Send(new GetProjectsQuery(ownerId));
        return Ok(ApiResponse<IEnumerable<ProjectDTO>>.Success(projects));
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var ownerId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var project = await mediator.Send(new GetProjectByIdQuery(id, ownerId));
        return Ok(ApiResponse<ProjectDTO>.Success(project));
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, UpdateProjectDTO updateProjectDTO)
    {
        var ownerId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        await mediator.Send(new UpdateProjectCommand(id, updateProjectDTO, ownerId));
        return Ok(ApiResponse.Success(204, "Project updated successfully."));
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var ownerId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        await mediator.Send(new DeleteProjectCommand(id, ownerId));
        return Ok(ApiResponse.Success(204, "Project deleted successfully."));
    }
}
