using System;

namespace PropertyManagement.Application.Features.DomainEvent;

public record ListDomainEventResponse
{
    public DateTime DateTime { get; init; }
    public string EventType { get; init; } = string.Empty;
    public string Message { get; init; } = string.Empty;
}
