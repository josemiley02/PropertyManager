using System;
using PropertyManagement.Application.DTOs.Paged;
using PropertyManagement.Application.Features.Host.Query.List;
using PropertyManagement.Common.Dto;

namespace PropertyManagement.Api.Endpoints.v1.Host;

public class ListHostEndpoint : Endpoint<QueryRequest, PagedResponse<ListHostResponse>> 
{
    public override void Configure()
    {
        Options(h => h.WithTags(RouteGroup.Host));
        Tags(RouteGroup.Host);
        Version(1);
        Get("/host/list");
        AllowAnonymous();
        Summary(f => f.Summary = "Listing all hosts");
    }

    public override async Task HandleAsync(QueryRequest req, CancellationToken ct)
    {
        var query = new ListHostQuery
        {
            QueryRequest = req
        };
        var data = await query.ExecuteAsync(ct);
        await SendAsync(data);
    }
}
