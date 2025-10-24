using System;
using PropertyManagement.Application.Features.Booking.Command.Delete;
using PropertyManagement.Common.Dto;

namespace PropertyManagement.Api.Endpoints.v1.Booking;

public class DeleteBookingEndpoint : Endpoint<DeleteBookingCommand, Response<NoContentData>>
{
    public override void Configure()
    {
        Options(b => b.WithTags(RouteGroup.Booking));
        Tags(RouteGroup.Booking);
        Version(1);
        Delete("booking/delete");
        AllowAnonymous();
        Summary(f => f.Summary = "Deleting a Booking");
    }
    public override async Task HandleAsync(DeleteBookingCommand req, CancellationToken ct)
    {
        var data = await req.ExecuteAsync(ct);
        await SendAsync(data);
    }
}
