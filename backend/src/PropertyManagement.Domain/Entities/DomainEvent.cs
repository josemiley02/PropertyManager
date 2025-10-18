using System;
using PropertyManagement.Domain.Entities.Identity;
using PropertyManagement.Domain.Enums;

namespace PropertyManagement.Domain.Entities;

public class DomainEvent : Entity<long>
{
    public EventType EventType { get; set; }
    public DateTime OccurredOn { get; set; } = DateTime.UtcNow;
    public string Description { get; set; } = string.Empty;
    public long PropertyId { get; set; }
    public Property Property { get; set; } = null!;
}
