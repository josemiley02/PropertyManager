using System;
using System.Linq.Dynamic;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PropertyManagement.Application.DTOs.Paged;
using PropertyManagement.Application.Extensions;
using PropertyManagement.Infrastructure.Abstractions;

namespace PropertyManagement.Application.Features.Booking.Query.List;

public class ListBookingQueryHandler : CoreQueryHandler<ListBookingQuery, PagedResponse<ListBookingResponse>>
{
    private readonly ILogger<ListBookingQueryHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    public ListBookingQueryHandler(ILogger<ListBookingQueryHandler> logger, IUnitOfWork unitOfWork) : base(unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }
    public override async Task<PagedResponse<ListBookingResponse>> ExecuteAsync(ListBookingQuery command, CancellationToken ct = default)
    {
        _logger.LogInformation($"{nameof(ExecuteAsync)} | Execution started");
        var bookingRepository = _unitOfWork.DbRepository<Domain.Entities.Booking>();
        var include = new List<Expression<Func<Domain.Entities.Booking, object>>>()
        {
            b => b.Property
        };
        var bookings = await bookingRepository.GetAllFiltered(includes: include, req: command.QueryRequest)
            .Select(b => new ListBookingResponse
            {
                Id = b.Id,
                CheckIn = b.CheckIn,
                CheckOut = b.CheckOut,
                Property = new DTOs.PropertyDto { Id = b.PropertyId, Name = b.Property.Name }
            }).ToPagedResultAsync(command.QueryRequest.Page, command.QueryRequest.PerPage);
        _logger.LogInformation($"{nameof(ExecuteAsync)} | Execution completed");
        return bookings;
    }
}
