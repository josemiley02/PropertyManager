using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PropertyManagement.Infrastructure.Abstractions;

namespace PropertyManagement.Application.Features.DomainEvent;

public class ListDomainEventCommandHandler : CoreCommandHandler<ListDomainEventCommand, IEnumerable<ListDomainEventResponse>>
{
    private readonly ILogger<ListDomainEventCommandHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    public ListDomainEventCommandHandler(ILogger<ListDomainEventCommandHandler> logger, IUnitOfWork unitOfWork) : base(unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }
    public override async Task<IEnumerable<ListDomainEventResponse>> ExecuteAsync(ListDomainEventCommand command, CancellationToken ct = default)
    {
        _logger.LogInformation($"{nameof(ExecuteAsync)} | Execution started");
        var domainEventRepository = _unitOfWork.DbRepository<Domain.Entities.DomainEvent>();
        var events = await domainEventRepository.GetAll()
            .Select(e => new ListDomainEventResponse
            {
                DateTime = e.OccurredOn,
                EventType = e.EventType.ToString(),
                Message = e.Description
            }).ToListAsync();
        _logger.LogInformation($"{nameof(ExecuteAsync)} | Execution completed");
        return events;
    }
}
