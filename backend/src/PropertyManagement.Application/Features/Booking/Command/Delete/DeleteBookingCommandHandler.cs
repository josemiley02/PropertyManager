using System;
using System.Linq.Expressions;
using Microsoft.Extensions.Logging;
using PropertyManagement.Application.Abstractions;
using PropertyManagement.Common.Dto;
using PropertyManagement.Domain.Enums;
using PropertyManagement.Infrastructure.Abstractions;

namespace PropertyManagement.Application.Features.Booking.Command.Delete;

public class DeleteBookingCommandHandler : CoreCommandHandler<DeleteBookingCommand, Response<NoContentData>>
{
    private readonly IDomainEventService _domainEvent;
    private readonly ILogger<DeleteBookingCommandHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    public DeleteBookingCommandHandler(ILogger<DeleteBookingCommandHandler> logger, IUnitOfWork unitOfWork, IDomainEventService domainEvent) : base(unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _domainEvent = domainEvent;
    }
    public override async Task<Response<NoContentData>> ExecuteAsync(DeleteBookingCommand command, CancellationToken ct = default)
    {
        _logger.LogInformation($"{nameof(ExecuteAsync)} | Execution started");
        var propertyRepository = _unitOfWork.DbRepository<Domain.Entities.Property>();
        var property = await propertyRepository.FirstAsync(filters: p => p.Id == command.PropertyId);
        if (property is null)
        {
            _logger.LogError($"Property with id {command.PropertyId} not found");
            ThrowError($"Property with id {command.PropertyId} not found", 404);
        }
        var bookingRepository = _unitOfWork.DbRepository<Domain.Entities.Booking>();
        var bookingInclude = new List<Expression<Func<Domain.Entities.Booking, object>>>()
        {
            b => b.Property
        };
        var booking = await bookingRepository.FirstAsync(filters: b => b.Id == command.BookingId);
        if (booking is null)
        {
            _logger.LogError($"Booking with id {command.BookingId} not found");
            ThrowError($"Booking with id {command.BookingId} not found", 404);
        }
        await bookingRepository.DeleteAsync(booking);
        await _domainEvent.AddEventAsync(
            EventType.BookingCancelled,
            property.Id,
            $"A booking was deleted today: {DateTime.UtcNow}"
        );
        _logger.LogInformation($"{nameof(ExecuteAsync)} | Execution completed");
        await _unitOfWork.SaveChangesAsync();
        return Response<NoContentData>.SuccessWithOutData("OK");
    }
}
