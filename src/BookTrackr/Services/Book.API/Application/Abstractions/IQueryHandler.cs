﻿using Book.API.Domain.Abstractions;
using MediatR;

namespace Book.API.Application.Abstractions;

public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>;
