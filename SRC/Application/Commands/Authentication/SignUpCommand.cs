using TaskManager.Application.DTOs.Authentication;

namespace TaskManager.Application.Commands.Authentication;

public record SignUpCommand(SignUpDTO SignUpData) : ICommand;
