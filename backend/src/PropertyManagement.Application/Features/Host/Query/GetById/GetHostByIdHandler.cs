using System;
using Microsoft.Extensions.Logging;
using PropertyManagement.Infrastructure.Abstractions;

namespace PropertyManagement.Application.Features.Host.Query.GetById;

public class GetHostByIdHandler : CoreQueryHandler<GetHostByIdCommand, GetHostByIdResponse>
{
    private readonly ILogger<GetHostByIdHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public GetHostByIdHandler(ILogger<GetHostByIdHandler> logger, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }
    public override async Task<GetHostByIdResponse> ExecuteAsync(GetHostByIdCommand command, CancellationToken ct = default)
    {
        _logger.LogInformation($"{nameof(ExecuteAsync)} | Execution started");
        var hostRepository = _unitOfWork.DbRepository<Domain.Entities.Host>();
        var host = await hostRepository.FirstAsync(filters: h => h.Id == command.Id);
        if (host is null)
        {
            _logger.LogError($"The host with id {command.Id} does'nt exists");
            ThrowError($"The host with id {command.Id} does'nt exists", 404);
        }
        return new GetHostByIdResponse
        {
            Id = host.Id,
            Name = host.FullName,
            Email = host.Email,
            Phone = host.Phone
        };
    }
}
