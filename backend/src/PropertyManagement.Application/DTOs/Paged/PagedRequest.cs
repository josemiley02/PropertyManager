using System;

namespace PropertyManagement.Application.DTOs.Paged;

public record PagedRequest
{
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
}
