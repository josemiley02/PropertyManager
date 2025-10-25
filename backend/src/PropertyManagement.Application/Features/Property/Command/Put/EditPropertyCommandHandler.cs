using System;
using Microsoft.Extensions.Logging;
using PropertyManagement.Application.Abstractions;
using PropertyManagement.Infrastructure.Abstractions;

namespace PropertyManagement.Application.Features.Property.Command.Put;

public class EditPropertyCommandHandler : CoreCommandHandler<EditPropertyCommad, EditPropertyResponse>
{
    private readonly IDomainEventService _domainEvent;
    private readonly ILogger<EditPropertyCommandHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    public EditPropertyCommandHandler(ILogger<EditPropertyCommandHandler> logger, IUnitOfWork unitOfWork, IDomainEventService domainEvent) : base(unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _domainEvent = domainEvent;
    }
    public override async Task<EditPropertyResponse> ExecuteAsync(EditPropertyCommad command, CancellationToken ct = default)
    {
        _logger.LogInformation($"{nameof(ExecuteAsync)} | Execution started");
        var propertyRepository = _unitOfWork.DbRepository<Domain.Entities.Property>();
        var property = await propertyRepository.FirstAsync(filters: p => p.Id == command.Id);
        if (property is null)
        {
            _logger.LogError($"Property with Id: {command.Id} not found");
            ThrowError($"Property with Id: {command.Id} not found", 404);
        }
        property.Name = command.Name;
        property.Address = command.Address;
        property.PricePerNight = command.PricePerNight;
        await propertyRepository.UpdateAsync(property);
        await _domainEvent.AddEventAsync(
            Domain.Enums.EventType.PropertyCreated,
            property.Id,
            $"The Property {property.Name} was updated");
        _logger.LogInformation($"{nameof(ExecuteAsync)} | Execution completed");
        await _unitOfWork.SaveChangesAsync();
        return new EditPropertyResponse
        {
            Name = property.Name,
            Address = property.Address,
            PricePerNight = property.PricePerNight
        };
    }
}
