using System;
using PropertyManagement.Application.DTOs;

namespace PropertyManagement.Application.Features.Host.Query.List;

public record ListHostResponse
{
    public long Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public IEnumerable<PropertyDto> Properties { get; set; } = new List<PropertyDto>(); 
}
