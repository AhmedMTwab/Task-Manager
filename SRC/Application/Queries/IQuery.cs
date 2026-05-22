using MediatR;

namespace TaskManager.Application.Queries;

public interface IQuery<TResponse> : IRequest<TResponse>
{
}