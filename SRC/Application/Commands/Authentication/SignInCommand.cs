using TaskManager.Application.DTOs.Authentication;

namespace TaskManager.Application.Commands.Authentication;

public record SignInCommand(SignInDTO SignInData) : ICommand<TokenDTO>;
