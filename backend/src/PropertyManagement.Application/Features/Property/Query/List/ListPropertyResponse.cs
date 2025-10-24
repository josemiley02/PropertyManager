using PropertyManagement.Application.DTOs;

namespace PropertyManagement.Application.Features.Property.Query.List;

public record ListPropertyResponse
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public double PricePerNight { get; set; }
    public HostDto Host { get; init; } = null!;
}
