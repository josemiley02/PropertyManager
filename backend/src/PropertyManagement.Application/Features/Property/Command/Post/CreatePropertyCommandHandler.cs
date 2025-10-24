using System;
using Microsoft.Extensions.Logging;
using PropertyManagement.Infrastructure.Abstractions;

namespace PropertyManagement.Application.Features.Property.Command.Post;

public class CreatePropertyCommandHandler : CoreCommandHandler<CreatePropertyCommand, CreatePropertyResponse>
{
    private readonly ILogger<CreatePropertyCommandHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    public CreatePropertyCommandHandler(ILogger<CreatePropertyCommandHandler> logger, IUnitOfWork unitOfWork) : base(unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }
    public override async Task<CreatePropertyResponse> ExecuteAsync(CreatePropertyCommand command, CancellationToken ct = default)
    {
        _logger.LogInformation($"{nameof(ExecuteAsync)} | Execution started");
        var propertyRepository = _unitOfWork.DbRepository<Domain.Entities.Property>();
        var hostRepository = _unitOfWork.DbRepository<Domain.Entities.Host>();
        var host = await hostRepository.FirstAsync(filters: h => h.Id == command.HostId);
        if (host is null)
        {
            _logger.LogError($"Host with Id: {command.HostId} not found");
            ThrowError($"Host with Id: {command.HostId} not found", 404);
        }
        var property = new Domain.Entities.Property
        {
            Name = command.Name,
            Address = command.Address,
            PricePerNight = command.PricePerNight,
            HostId = host.Id,
            Host = host
        };
        await propertyRepository.SaveAsync(property);
        _logger.LogInformation($"{nameof(ExecuteAsync)} | Execution completed");
        await _unitOfWork.SaveChangesAsync();
        return new CreatePropertyResponse
        {
            Id = property.Id,
            Address = property.Address,
            Name = property.Name,
            PricePerNight = property.PricePerNight,
            Host = new DTOs.HostDto
            {
                Id = property.HostId,
                Name = property.Host.FullName
            }
        };
    }
}
