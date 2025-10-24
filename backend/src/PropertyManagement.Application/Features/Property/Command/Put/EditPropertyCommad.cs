using System;

namespace PropertyManagement.Application.Features.Property.Command.Put;

public record EditPropertyCommad : ICommand<EditPropertyResponse>
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public double PricePerNight { get; set; }
}
