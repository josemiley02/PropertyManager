using System;
using Microsoft.Extensions.Logging;
using PropertyManagement.Application.Abstractions;
using PropertyManagement.Common.Dto;
using PropertyManagement.Infrastructure.Abstractions;

namespace PropertyManagement.Application.Features.Property.Command.Delete;

public class DeletePropertyCommandHandler : CoreCommandHandler<DeletePropertyCommand, Response<NoContentData>>
{
    private readonly IDomainEventService _domainEvent;
    private readonly ILogger<DeletePropertyCommandHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    public DeletePropertyCommandHandler(IUnitOfWork unitOfWork, ILogger<DeletePropertyCommandHandler> logger, IDomainEventService domainEvent) : base(unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _domainEvent = domainEvent;
    }
    public override async Task<Response<NoContentData>> ExecuteAsync(DeletePropertyCommand command, CancellationToken ct = default)
    {
        _logger.LogInformation($"{nameof(ExecuteAsync)} | Execution started");
        var propertyRepository = _unitOfWork.DbRepository<Domain.Entities.Property>();
        var property = await propertyRepository.FirstAsync(filters: p => p.Id == command.Id);
        if (property is null)
        {
            _logger.LogError($"Property with id {command.Id} not found");
            ThrowError($"Property with id {command.Id} not found", 404);
        }
        await propertyRepository.DeleteAsync(property);
        await _domainEvent.AddEventAsync(
            Domain.Enums.EventType.PropertyDeleted,
            property.Id,
            $"Deleted the Property {property.Name}"
        );
        _logger.LogInformation($"{nameof(ExecuteAsync)} | Execution completed");
        await _unitOfWork.SaveChangesAsync();
        return Response<NoContentData>.SuccessWithOutData("OK");
    }
}
