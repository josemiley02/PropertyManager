using System;

namespace PropertyManagement.Application.Features.Host.Command.Post;

public record CreateHostCommand : ICommand<CreateHostResponse>
{
    public string FullName { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string Phone { get; init; } = string.Empty;
}
