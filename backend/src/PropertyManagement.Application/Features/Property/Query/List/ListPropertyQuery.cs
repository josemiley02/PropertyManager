using PropertyManagement.Application.DTOs.Paged;
using PropertyManagement.Common.Dto;

namespace PropertyManagement.Application.Features.Property.Query.List;

public record ListPropertyQuery : ICommand<PagedResponse<ListPropertyResponse>>
{
    public QueryRequest QueryRequest { get; set; } = null!;
}
