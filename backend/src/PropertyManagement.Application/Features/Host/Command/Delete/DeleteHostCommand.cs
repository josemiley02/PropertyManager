using System;
using PropertyManagement.Common.Dto;

namespace PropertyManagement.Application.Features.Host.Command.Delete;

public record DeleteHostCommand : ICommand<Response<NoContentData>>
{
    public long Id { get; init; }
}
