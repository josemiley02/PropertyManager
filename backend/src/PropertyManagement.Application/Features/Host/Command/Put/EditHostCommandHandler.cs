using System;
using Microsoft.Extensions.Logging;
using PropertyManagement.Infrastructure.Abstractions;

namespace PropertyManagement.Application.Features.Host.Command.Put;

public class EditHostCommandHandler : CoreCommandHandler<EditHostCommand, EditHostResponse>
{
    private readonly ILogger<EditHostCommandHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    public EditHostCommandHandler(ILogger<EditHostCommandHandler> logger, IUnitOfWork unitOfWork) : base(unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }
    public override async Task<EditHostResponse> ExecuteAsync(EditHostCommand command, CancellationToken ct = default)
    {
        _logger.LogInformation($"{nameof(ExecuteAsync)} | Execution started");
        var hostRepository = _unitOfWork.DbRepository<Domain.Entities.Host>();
        var host = await hostRepository.FirstAsync(filters: h => h.Id == command.Id);
        if (host is null)
        {
            _logger.LogError($"Host with id {command.Id} not found");
            ThrowError($"Host with id {command.Id} not found", 404);
        }
        host.FullName = command.FullName;
        host.Email = command.Email;
        host.Phone = command.Phone;
        await hostRepository.UpdateAsync(host);
        _logger.LogInformation($"{nameof(ExecuteAsync)} | Execution completed");
        await _unitOfWork.SaveChangesAsync();
        return new EditHostResponse
        {
            FullName = host.FullName,
            Email = host.Email,
            Phone = host.Phone
        };
    }
}
