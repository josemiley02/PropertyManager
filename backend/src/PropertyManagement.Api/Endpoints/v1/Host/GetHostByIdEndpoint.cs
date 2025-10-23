using System;
using PropertyManagement.Application.Features.Host.Query.GetById;

namespace PropertyManagement.Api.Endpoints.v1.Host;

public class GetHostByIdEndpoint : Endpoint<GetHostByIdCommand, GetHostByIdResponse>
{
    public override void Configure()
    {
        Options(x => x.WithTags(RouteGroup.Host));
        Tags(RouteGroup.Host);
        Version(1);
        Get("host/{id}");
        AllowAnonymous();
        Summary(f => f.Summary = "Getting Host by Id");
    }
    public override async Task HandleAsync(GetHostByIdCommand req, CancellationToken ct)
    {
        var data = await req.ExecuteAsync(ct);
        await SendAsync(data);
    }
}
