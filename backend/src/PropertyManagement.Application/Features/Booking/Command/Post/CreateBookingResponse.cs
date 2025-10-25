using System;

namespace PropertyManagement.Application.Features.Booking.Command.Post;

public record CreateBookingResponse
{
    public long Id { get; init; }
    public DateTime CheckIn { get; init; }
    public DateTime CheckOut { get; init; }
    public double TotalPrice { get; init; }
    public long PropertyId { get; init; }
}
