using MediatR;

namespace TaskManager.Application.Commands;

public interface ICommand : IRequest<Unit>
{
}

public interface ICommand<TResponse> : IRequest<TResponse>
{
}