using PropertyManagement.Application.DTOs;

namespace PropertyManagement.Application.Features.Booking.Query.List;

public record ListBookingResponse
{
    public long Id { get; init; }
    public PropertyDto Property { get; init; } = null!;
    public DateTime CheckIn { get; init; }
    public DateTime CheckOut { get; init; }
}
