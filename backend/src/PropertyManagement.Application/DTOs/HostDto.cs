using System;

namespace PropertyManagement.Application.DTOs;

public record HostDto
{
    public long Id { get; init; }
    public string Name { get; init; } = string.Empty;
}
