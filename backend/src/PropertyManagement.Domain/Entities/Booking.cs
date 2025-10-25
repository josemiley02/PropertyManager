using System;
using PropertyManagement.Domain.Entities.Identity;

namespace PropertyManagement.Domain.Entities;

public class Booking : Entity<long>
{
    public DateTime CheckIn { get; set; }
    public DateTime CheckOut { get; set; }
    public double TotalPrice { get; set; }
    public long PropertyId { get; set; }
    public Property Property { get; set; } = null!;
}
