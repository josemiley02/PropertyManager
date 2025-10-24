
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PropertyManagement.Application.DTOs.Paged;
using PropertyManagement.Application.Extensions;
using PropertyManagement.Infrastructure.Abstractions;

namespace PropertyManagement.Application.Features.Property.Query.List;

public class ListPropertyQueryHandler : CoreQueryHandler<ListPropertyQuery, PagedResponse<ListPropertyResponse>>
{
    private readonly ILogger<ListPropertyQueryHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    public ListPropertyQueryHandler(ILogger<ListPropertyQueryHandler> logger, IUnitOfWork unitOfWork) : base(unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }
    public override async Task<PagedResponse<ListPropertyResponse>> ExecuteAsync(ListPropertyQuery command, CancellationToken ct = default)
    {
        _logger.LogInformation($"{nameof(ExecuteAsync)} | Execution started");
        var propertyRepository = _unitOfWork.DbRepository<Domain.Entities.Property>();
        var include = new List<Expression<Func<Domain.Entities.Property, object>>>()
        {
            p => p.Host
        };
        var properties = await propertyRepository.GetAllFiltered(includes: include, req: command.QueryRequest)
            .Where(x => string.IsNullOrEmpty(command.QueryRequest.Query) ||
                    command.QueryRequest.Query.Contains(x.Name) ||
                    command.QueryRequest.Query.Contains(x.Address) ||
                    command.QueryRequest.Query.Contains(x.Host.FullName))
            .Select(p => new ListPropertyResponse
            {
                Id = p.Id,
                Name = p.Name,
                PricePerNight = p.PricePerNight,
                Host = new DTOs.HostDto
                {
                    Id = p.HostId,
                    Name = p.Name
                }
            }).ToPagedResultAsync(command.QueryRequest.Page, command.QueryRequest.PerPage);
        _logger.LogInformation($"{nameof(ExecuteAsync)} | Execution completed");
        return properties;
    }
}
