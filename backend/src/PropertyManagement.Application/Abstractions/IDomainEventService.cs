using System;
using PropertyManagement.Domain.Enums;

namespace PropertyManagement.Application.Abstractions;

public interface IDomainEventService
{
    Task AddEventAsync(EventType eventType, long propertyId, string description);
}
