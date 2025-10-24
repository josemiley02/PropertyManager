using System;
using PropertyManagement.Application.Features.Booking.Command.Post;

namespace PropertyManagement.Api.Endpoints.v1.Booking;

public class CreateBookingEndpoint : Endpoint<CreateBookingCommand, CreateBookingResponse>
{
    public override void Configure()
    {
        Options(b => b.WithTags(RouteGroup.Booking));
        Tags(RouteGroup.Booking);
        Version(1);
        Post("booking/post");
        AllowAnonymous();
        Summary(f => f.Summary = "Creating a Booking");
    }

    public override async Task HandleAsync(CreateBookingCommand req, CancellationToken ct)
    {
        var data = await req.ExecuteAsync(ct);
        await SendAsync(data);
    }
}
