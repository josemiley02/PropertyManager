using System;
using PropertyManagement.Application.Features.Host.Command.Post;

namespace PropertyManagement.Api.Endpoints.v1.Host;

public class CreateHostEndpoint : Endpoint<CreateHostCommand, CreateHostResponse>
{
    public override void Configure()
    {
        Options(h => h.WithTags(RouteGroup.Host));
        Tags(RouteGroup.Host);
        Version(1);
        Post("host/post");
        AllowAnonymous();
        Summary(f => f.Summary = "Creating a Host");
    }

    public override async Task HandleAsync(CreateHostCommand req, CancellationToken ct)
    {
        var data = await req.ExecuteAsync(ct);
        await SendAsync(data);
    }
}
