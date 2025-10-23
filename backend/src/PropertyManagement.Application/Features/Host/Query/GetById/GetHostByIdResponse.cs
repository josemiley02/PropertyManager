using System;

namespace PropertyManagement.Application.Features.Host.Query.GetById;

public record GetHostByIdResponse
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
}
