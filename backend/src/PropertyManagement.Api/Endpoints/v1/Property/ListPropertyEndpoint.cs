using System;
using PropertyManagement.Application.DTOs.Paged;
using PropertyManagement.Application.Features.Property.Query.List;
using PropertyManagement.Common.Dto;

namespace PropertyManagement.Api.Endpoints.v1.Property;

public class ListPropertyEndpoint : Endpoint<QueryRequest, PagedResponse<ListPropertyResponse>>
{
    public override void Configure()
    {
        Options(p => p.WithTags(RouteGroup.Property));
        Tags(RouteGroup.Property);
        Version(1);
        Get("property/list");
        AllowAnonymous();
        Summary(f => f.Summary = "Getting all Properties");
    }

    public override async Task HandleAsync(QueryRequest req, CancellationToken ct)
    {
        var query = new ListPropertyQuery
        {
            QueryRequest = req
        };
        var data = await query.ExecuteAsync(ct);
        await SendAsync(data);
    }
}
