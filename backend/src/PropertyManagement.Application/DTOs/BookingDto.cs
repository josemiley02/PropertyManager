using System;

namespace PropertyManagement.Application.DTOs;

public record BookingDto
{
    public long Id { get; set; }
    public DateTime CheckIn { get; set; }
    public DateTime CheckOut { get; set; }
}
