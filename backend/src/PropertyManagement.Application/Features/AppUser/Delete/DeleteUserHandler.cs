using System;
using System.Linq.Expressions;
using Microsoft.Extensions.Logging;
using PropertyManagement.Common.Dto;
using PropertyManagement.Infrastructure.Abstractions;

namespace PropertyManagement.Application.Features.AppUser.Delete;

public class DeleteUserCommandHandler : CoreCommandHandler<DeleteUserCommand, Response<NoContentData>>
{
    readonly IUnitOfWork _unitOfWork;
    readonly ILogger<DeleteUserCommandHandler> _logger;

    public DeleteUserCommandHandler(IUnitOfWork unitOfWork, ILogger<DeleteUserCommandHandler> logger) : base(unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async override Task<Response<NoContentData>> ExecuteAsync(DeleteUserCommand command, CancellationToken ct = default)
    {
        _logger.LogInformation($"{nameof(ExecuteAsync)} | Execution started");

        var userRepository = _unitOfWork.DbRepository<Domain.Entities.Identity.AppUser>();
        var filter = new Expression<Func<Domain.Entities.Identity.AppUser, bool>>[]
        {
            x => x.Id == command.Id,
        };

        var user = await userRepository.FirstAsync(useInactive: true, filters: filter);
        if (user is null)
        {
            _logger.LogError($"User with id {command.Id} does'nt exists");
            ThrowError($"User with id {command.Id} does'nt exists", 404);
        }
        await userRepository.DeleteAsync(user);
        await _unitOfWork.SaveChangesAsync();
        _logger.LogInformation($"{nameof(ExecuteAsync)} | Execution completed");

        return Response<NoContentData>.SuccessWithOutData("OK");
    }
}