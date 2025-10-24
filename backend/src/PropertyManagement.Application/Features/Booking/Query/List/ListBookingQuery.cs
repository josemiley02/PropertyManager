using System;
using PropertyManagement.Application.DTOs.Paged;
using PropertyManagement.Common.Dto;

namespace PropertyManagement.Application.Features.Booking.Query.List;

public record ListBookingQuery : ICommand<PagedResponse<ListBookingResponse>>
{
    public QueryRequest QueryRequest { get; init; } = null!;
}
