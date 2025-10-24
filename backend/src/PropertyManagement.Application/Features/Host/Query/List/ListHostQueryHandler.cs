using System;
using System.Linq.Expressions;
using Microsoft.Extensions.Logging;
using PropertyManagement.Application.DTOs.Paged;
using PropertyManagement.Application.Extensions;
using PropertyManagement.Infrastructure.Abstractions;

namespace PropertyManagement.Application.Features.Host.Query.List;

public class ListHostQueryHandler : CoreQueryHandler<ListHostQuery, PagedResponse<ListHostResponse>>
{
    private readonly ILogger<ListHostQueryHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    public ListHostQueryHandler(ILogger<ListHostQueryHandler> logger, IUnitOfWork unitOfWork) : base(unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }
    public override async Task<PagedResponse<ListHostResponse>> ExecuteAsync(ListHostQuery command, CancellationToken ct = default)
    {
        _logger.LogInformation($"{nameof(ExecuteAsync)} | Execution started");
        var hostRepository = _unitOfWork.DbRepository<Domain.Entities.Host>();
        var includes = new List<Expression<Func<Domain.Entities.Host, object>>>()
        {
            h => h.Properties
        };
        var hosts = await hostRepository.GetAllFiltered(req: command.QueryRequest, includes: includes)
            .Where(x => string.IsNullOrEmpty(command.QueryRequest.Query) ||
                    x.FullName.Contains(command.QueryRequest.Query) ||
                    x.Email.Contains(command.QueryRequest.Query) ||
                    x.Phone.Contains(command.QueryRequest.Query))
            .Select(h => new ListHostResponse
            {
                Id = h.Id,
                Email = h.Email,
                Phone = h.Phone,
                FullName = h.FullName,
                Properties = h.Properties.Select(p => new DTOs.PropertyDto
                {
                    Id = p.Id,
                    Name = p.Name
                })
            }).ToPagedResultAsync(command.QueryRequest.Page, command.QueryRequest.PerPage);
        _logger.LogInformation($"{nameof(ExecuteAsync)} | Execution completed");
        return hosts;
    }
}
