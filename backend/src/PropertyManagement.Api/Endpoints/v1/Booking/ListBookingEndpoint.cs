using System;
using PropertyManagement.Application.DTOs.Paged;
using PropertyManagement.Application.Features.Booking.Query.List;
using PropertyManagement.Common.Dto;

namespace PropertyManagement.Api.Endpoints.v1.Booking;

public class ListBookingEndpoint : Endpoint<QueryRequest, PagedResponse<ListBookingResponse>>
{
    public override void Configure()
    {
        Options(b => b.WithTags(RouteGroup.Booking));
        Tags(RouteGroup.Booking);
        Version(1);
        Get("booking/list");
        AllowAnonymous();
        Summary(f => f.Summary = "Listing all bookings");
    }

    public override async Task HandleAsync(QueryRequest req, CancellationToken ct)
    {
        var query = new ListBookingQuery { QueryRequest = req };
        var data = await query.ExecuteAsync(ct);
        await SendAsync(data);
    }
}
