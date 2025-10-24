using System;
using System.Linq.Expressions;
using Microsoft.Extensions.Logging;
using PropertyManagement.Infrastructure.Abstractions;

namespace PropertyManagement.Application.Features.Property.Query.GetById;

public class GetPropertyByIdCommandHandler : CoreCommandHandler<GetPropertyByIdCommand, GetPropertyByIdResponse>
{
    private readonly ILogger<GetPropertyByIdCommandHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    public GetPropertyByIdCommandHandler(IUnitOfWork unitOfWork, ILogger<GetPropertyByIdCommandHandler> logger) : base(unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }
    public override async Task<GetPropertyByIdResponse> ExecuteAsync(GetPropertyByIdCommand command, CancellationToken ct = default)
    {
        _logger.LogInformation($"{nameof(ExecuteAsync)} | Execution started");
        var propertyRepository = _unitOfWork.DbRepository<Domain.Entities.Property>();
        var include = new List<Expression<Func<Domain.Entities.Property, object>>>()
        {
            p => p.Host,
            p => p.Bookings
        };
        var property = await propertyRepository.FirstAsync(includes: include, filters: p => p.Id == command.Id);
        if (property is null)
        {
            _logger.LogError($"Property with id {command.Id} not found");
            ThrowError($"Property with id {command.Id} not found", 404);
        }
        return new GetPropertyByIdResponse
        {
            Name = property.Name,
            Address = property.Address,
            PricePerNight = property.PricePerNight,
            Host = new DTOs.HostDto
            {
                Id = property.Host.Id,
                Name = property.Host.FullName
            },
            Bookings = property.Bookings.Select(b => new DTOs.BookingDto
            {
                Id = b.Id,
                CheckIn = b.CheckIn,
                CheckOut = b.CheckOut,
            })
        };
    }
}
