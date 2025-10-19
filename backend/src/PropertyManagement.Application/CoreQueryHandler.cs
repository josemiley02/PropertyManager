using System;
using PropertyManagement.Infrastructure.Abstractions;

namespace PropertyManagement.Application;

public abstract class CoreQueryHandler<TRequest, TResponse> : CoreApplicationService<TRequest, TResponse> where TRequest : class, ICommand<TResponse>
{
    protected CoreQueryHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }
    protected CoreQueryHandler()
    {
    } 
    public abstract override Task<TResponse> ExecuteAsync(TRequest command, CancellationToken ct = default);
}