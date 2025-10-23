using System;

namespace PropertyManagement.Application.Features.Host.Query.GetById;

public record GetHostByIdCommand : ICommand<GetHostByIdResponse>
{
    public required long Id { get; set; }
}
