using System;
using PropertyManagement.Domain.Entities.Identity;

namespace PropertyManagement.Domain.Entities;

public class Property : Entity<long>
{
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public double PricePerNight { get; set; }
    public long HostId { get; set; }
    public Host Host { get; set; } = null!;
    public ICollection<Booking> Bookings = new HashSet<Booking>();
    public ICollection<DomainEvent> DomainEvents = new HashSet<DomainEvent>();
}
