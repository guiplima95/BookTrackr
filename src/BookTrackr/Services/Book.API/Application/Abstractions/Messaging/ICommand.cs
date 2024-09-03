using Book.API.Domain.Abstractions;
using MediatR;

namespace Book.API.Application.Abstractions.Menssaging;

public interface ICommand : IRequest<Result>;

public interface ICommand<TResponse> : IRequest<Result<TResponse>>, IBaseCommand;

public interface IBaseCommand
{
}