using System;
using PropertyManagement.Application.DTOs.Paged;
using PropertyManagement.Common.Dto;

namespace PropertyManagement.Application.Features.Host.Query.List;

public record ListHostQuery : ICommand<PagedResponse<ListHostResponse>>
{
    public QueryRequest QueryRequest { get; set; } = null!;
}
