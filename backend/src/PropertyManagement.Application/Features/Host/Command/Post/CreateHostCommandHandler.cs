using System;
using Microsoft.Extensions.Logging;
using PropertyManagement.Infrastructure.Abstractions;

namespace PropertyManagement.Application.Features.Host.Command.Post;

public class CreateHostCommandHandler : CoreCommandHandler<CreateHostCommand, CreateHostResponse>
{
    private readonly ILogger<CreateHostCommandHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    public CreateHostCommandHandler(ILogger<CreateHostCommandHandler> logger, IUnitOfWork unitOfWork) : base(unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }
    public override async Task<CreateHostResponse> ExecuteAsync(CreateHostCommand command, CancellationToken ct = default)
    {
        _logger.LogInformation($"{nameof(ExecuteAsync)} | Execution started");
        var hostRepository = _unitOfWork.DbRepository<Domain.Entities.Host>();
        var host = new Domain.Entities.Host
        {
            FullName = command.FullName,
            Email = command.Email,
            Phone = command.Phone
        };
        await hostRepository.SaveAsync(host);
        _logger.LogInformation($"{nameof(ExecuteAsync)} | Execution completed");
        await _unitOfWork.SaveChangesAsync();
        return new CreateHostResponse
        {
            Id = host.Id,
            FullName = host.FullName,
            Email = host.Email,
            Phone = host.Phone
        };
    }
}
