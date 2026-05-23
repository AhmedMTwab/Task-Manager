using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.Commands.Task;
using TaskManager.Application.Queries.Task;
using TaskManager.Application.DTOs;
using TaskManager.Application.DTOs.Task;

namespace TaskManager.Presentation.Controllers;

[Route("api/Project/{projectId:guid}/[controller]")]
[ApiController]
[Authorize]
public class TaskController(ISender mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll(Guid projectId)
    {
        var ownerId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var tasks = await mediator.Send(new GetProjectTasksQuery(projectId, ownerId));
        return Ok(ApiResponse<IEnumerable<TaskDTO>>.Success(tasks));
    }

    [HttpPost]
    public async Task<IActionResult> Create(Guid projectId, CreateTaskDTO createTaskDTO)
    {
        var ownerId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        await mediator.Send(new CreateTaskCommand(projectId, createTaskDTO, ownerId));
        return Ok(ApiResponse.Success(201, "Task created successfully."));
    }
}
