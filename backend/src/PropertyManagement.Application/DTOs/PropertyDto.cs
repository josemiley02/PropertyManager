using System;

namespace PropertyManagement.Application.DTOs;

public record PropertyDto
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
}
