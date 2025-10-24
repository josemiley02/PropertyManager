using System;

namespace PropertyManagement.Application.Features.Host.Command.Post;

public record CreateHostResponse
{
    public long Id { get; init; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
}
