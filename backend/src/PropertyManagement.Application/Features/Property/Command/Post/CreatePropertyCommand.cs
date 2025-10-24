using System;
using PropertyManagement.Application.Features.Host.Command.Post;

namespace PropertyManagement.Application.Features.Property.Command.Post;

public record CreatePropertyCommand : ICommand<CreatePropertyResponse>
{
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public double PricePerNight { get; set; }
    public long HostId { get; set; }
}
