using System;

namespace PropertyManagement.Application.Features.Host.Command.Put;

public record EditHostCommand : ICommand<EditHostResponse>
{
    public long Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
}
