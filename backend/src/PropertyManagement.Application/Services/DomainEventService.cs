using System;
using System.Text.Json;
using PropertyManagement.Application.Abstractions;
using PropertyManagement.Domain.Entities;
using PropertyManagement.Domain.Enums;
using PropertyManagement.Infrastructure.Abstractions;

namespace PropertyManagement.Application.Services;

[RegisterService<IDomainEventService>(LifeTime.Scoped)]
public class DomainEventService : IDomainEventService
{
    private readonly IUnitOfWork _unitOfWork;

    public DomainEventService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task AddEventAsync(EventType eventType, long propertyId, string description)
    {
        var repo = _unitOfWork.DbRepository<DomainEvent>();

        var domainEvent = new DomainEvent
        {
            EventType = eventType,
            PropertyId = propertyId,
            Description = description,
            OccurredOn = DateTime.UtcNow
        };

        await repo.SaveAsync(domainEvent);
        await _unitOfWork.SaveChangesAsync();
    }
}
