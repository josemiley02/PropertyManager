using PropertyManagement.Common.Dto;

namespace PropertyManagement.Application.Features.Booking.Command.Delete;

public record DeleteBookingCommand : ICommand<Response<NoContentData>>
{
    public long PropertyId { get; init; }
    public long BookingId{ get; init; }
}
