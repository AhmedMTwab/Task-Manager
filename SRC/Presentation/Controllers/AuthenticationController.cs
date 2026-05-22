using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.Commands.Authentication;
using TaskManager.Application.DTOs;
using TaskManager.Application.DTOs.Authentication;

namespace TaskManager.Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthenticationController(ISender mediator) : ControllerBase
{
    [HttpPost("SignUp")]
    public async Task<IActionResult> SignUp(SignUpDTO signUpDTO)
    {
        await mediator.Send(new SignUpCommand(signUpDTO));
        return Ok(ApiResponse.Success(201, "Registered successfully."));
    }

    [HttpPost("SignIn")]
    public async Task<IActionResult> SignIn(SignInDTO signInDTO)
    {
        var token = await mediator.Send(new SignInCommand(signInDTO));
        return Ok(ApiResponse<TokenDTO>.Success(token));
    }
}
