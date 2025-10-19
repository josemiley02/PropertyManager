using System;
using PropertyManagement.Infrastructure.Abstractions;

namespace PropertyManagement.Application;

public abstract class CoreCommandHandler<TRequest, TResponse> : CoreApplicationService<TRequest, TResponse> where TRequest : class , ICommand<TResponse>
{
    protected CoreCommandHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }
    protected CoreCommandHandler()
    {
    }
    public abstract override Task<TResponse> ExecuteAsync(TRequest command, CancellationToken ct = default);
}