using System;
using Microsoft.Extensions.Logging;
using PropertyManagement.Infrastructure.Abstractions;

namespace PropertyManagement.Application.Features.Booking.Command.Post;

public class CreateBookingCommandHandler : CoreCommandHandler<CreateBookingCommand, CreateBookingResponse>
{
    private readonly ILogger<CreateBookingCommandHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    public CreateBookingCommandHandler(ILogger<CreateBookingCommandHandler> logger, IUnitOfWork unitOfWork) : base(unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }
    public override async Task<CreateBookingResponse> ExecuteAsync(CreateBookingCommand command, CancellationToken ct = default)
    {
        _logger.LogInformation($"{nameof(ExecuteAsync)} | Execution started");
        var bookingRepository = _unitOfWork.DbRepository<Domain.Entities.Booking>();
        var propertyRepository = _unitOfWork.DbRepository<Domain.Entities.Property>();
        var property = await propertyRepository.FirstAsync(filters: p => p.Id == command.PropertyId);
        if (property is null)
        {
            _logger.LogError($"The property with ID {command.PropertyId} not found");
            ThrowError($"The property with ID {command.PropertyId} not found", 400);
        }
        if (command.CheckIn >= command.CheckOut)
        {
            _logger.LogError("The date for check in cannot be greater that the date for check out");
            ThrowError("The date for check in cannot be greater that the date for check out", 400);
        }
        var booking = new Domain.Entities.Booking
        {
            CheckIn = command.CheckIn,
            CheckOut = command.CheckOut,
            PropertyId = property.Id,
            Property = property
        };
        await bookingRepository.SaveAsync(booking);
        _logger.LogInformation($"{nameof(ExecuteAsync)} | Execution completed");
        await _unitOfWork.SaveChangesAsync();
        return new CreateBookingResponse
        {
            Id = booking.Id,
            CheckIn = booking.CheckIn,
            CheckOut = booking.CheckOut,
            PropertyId = booking.PropertyId
        };
    }
}
