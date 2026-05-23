using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.Commands.Project;
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
}
