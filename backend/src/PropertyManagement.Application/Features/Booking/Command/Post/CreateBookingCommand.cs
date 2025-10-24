using System;

namespace PropertyManagement.Application.Features.Booking.Command.Post;

public record CreateBookingCommand : ICommand<CreateBookingResponse>
{
    public long PropertyId { get; set; }
    public DateTime CheckIn { get; init; }
    public DateTime CheckOut { get; init; }
}
