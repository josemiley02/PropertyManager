using System;
using PropertyManagement.Application.DTOs;

namespace PropertyManagement.Application.Features.Property.Query.GetById;

public record GetPropertyByIdResponse
{
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public double PricePerNight { get; set; }
    public HostDto Host { get; init; } = null!;
    public IEnumerable<BookingDto> Bookings { get; init; } = new List<BookingDto>();
}
