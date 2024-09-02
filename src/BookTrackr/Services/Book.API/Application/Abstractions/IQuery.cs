using Book.API.Domain.Abstractions;
using MediatR;

namespace Book.API.Application.Abstractions;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>;