using System;
using PropertyManagement.Application.Features.Property.Query.GetById;

namespace PropertyManagement.Api.Endpoints.v1.Property;

public class GetPropertyByIdEndpoint : Endpoint<GetPropertyByIdCommand, GetPropertyByIdResponse>
{
    public override void Configure()
    {
        Options(b => b.WithTags(RouteGroup.Property));
        Tags(RouteGroup.Property);
        Version(1);
        Get("property/{id}/get");
        AllowAnonymous();
        Summary(f => f.Summary = "Getting Property General Data");
    }

    public override async Task HandleAsync(GetPropertyByIdCommand req, CancellationToken ct)
    {
        var data = await req.ExecuteAsync(ct);
        await SendAsync(data);
    }
}
