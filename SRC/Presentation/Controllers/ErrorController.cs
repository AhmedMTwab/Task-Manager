using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.DTOs;
using TaskManager.Domain.Exceptions;

namespace TaskManager.Presentation.Controllers;

[ApiController]
[ApiExplorerSettings(IgnoreApi = true)]
[AllowAnonymous]
public class ErrorController : ControllerBase
{
    private readonly ILogger<ErrorController> _logger;

    public ErrorController(ILogger<ErrorController> logger)
    {
        _logger = logger;
    }

    [Route("/error")]
    public IActionResult HandleException()
    {
        var exceptionFeature = HttpContext.Features.Get<IExceptionHandlerFeature>();

        if (exceptionFeature is null)
        {
            return StatusCode(500, ApiResponse.Fail(500, "An unexpected error occurred."));
        }

        var exception = exceptionFeature.Error;

        var (statusCode, message, errors) = exception switch
        {
            BadRequestException ex => (400, ex.Message, ex.ValidationErrors),
            UnauthorizedException ex => (401, ex.Message, (Dictionary<string, List<string>>?)null),
            ForbiddenException ex => (403, ex.Message, (Dictionary<string, List<string>>?)null),
            NotFoundException ex => (404, ex.Message, (Dictionary<string, List<string>>?)null),
            ConflictException ex => (409, ex.Message, (Dictionary<string, List<string>>?)null),

            _ => (500, "An internal server error occurred.", (Dictionary<string, List<string>>?)null)
        };

        if (statusCode >= 500)
            _logger.LogError(exception, "Unhandled exception: {Message}", exception.Message);
        else
            _logger.LogWarning(exception, "Client error ({StatusCode}): {Message}", statusCode, exception.Message);

        return StatusCode(statusCode, ApiResponse.Fail(statusCode, message, errors));
    }


    [Route("/error/{statusCode:int}")]
    public IActionResult HandleStatusCode([FromRoute] int statusCode)
    {
        var message = statusCode switch
        {
            400 => "Bad request.",
            401 => "You are not authenticated. Please provide a valid token.",
            403 => "You do not have permission to access this resource.",
            404 => "The requested resource was not found.",
            405 => "HTTP method not allowed for this endpoint.",
            408 => "The request timed out. Please try again.",
            409 => "A conflict occurred with the current state of the resource.",
            415 => "Unsupported media type.",
            429 => "Too many requests. Please slow down.",
            500 => "An internal server error occurred.",
            502 => "Bad gateway.",
            503 => "Service is temporarily unavailable. Please try again later.",
            _ => "An error occurred."
        };

        _logger.LogWarning("Status code page triggered: {StatusCode} — {Path}",
            statusCode, HttpContext.Request.Path);

        return StatusCode(statusCode, ApiResponse.Fail(statusCode, message));
    }
}
