using System;
using Microsoft.Extensions.Logging;
using PropertyManagement.Common.Dto;
using PropertyManagement.Infrastructure.Abstractions;

namespace PropertyManagement.Application.Features.Host.Command.Delete;

public class DeleteHostCommandHandler : CoreCommandHandler<DeleteHostCommand, Response<NoContentData>>
{
    private readonly ILogger<DeleteHostCommandHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    public DeleteHostCommandHandler(ILogger<DeleteHostCommandHandler> logger, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }
    public override async Task<Response<NoContentData>> ExecuteAsync(DeleteHostCommand command, CancellationToken ct = default)
    {
        _logger.LogInformation($"{nameof(ExecuteAsync)} | Execution started");
        var hostRepository = _unitOfWork.DbRepository<Domain.Entities.Host>();
        var host = await hostRepository.FirstAsync(filters: h => h.Id == command.Id);
        if (host is null)
        {
            _logger.LogError($"Host with id {command.Id} not found");
            ThrowError($"Host with id {command.Id} not found", 404);
        }
        await hostRepository.DeleteAsync(host);
        _logger.LogInformation($"{nameof(ExecuteAsync)} | Execution completed");
        await _unitOfWork.SaveChangesAsync();
        return Response<NoContentData>.SuccessWithOutData("OK");
    }
}
