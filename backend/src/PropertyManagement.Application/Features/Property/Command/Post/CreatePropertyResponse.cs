using System;
using PropertyManagement.Application.DTOs;

namespace PropertyManagement.Application.Features.Property.Command.Post;

public record CreatePropertyResponse
{
    public long Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Address { get; init; } = string.Empty;
    public double PricePerNight { get; init; }
    public HostDto Host { get; init; } = null!;
}
