using System;

namespace PropertyManagement.Application.Features.Host.Command.Put;

public record EditHostResponse
{
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
}
