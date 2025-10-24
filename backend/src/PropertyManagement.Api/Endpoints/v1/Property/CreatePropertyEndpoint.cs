using System;
using PropertyManagement.Application.Features.Property.Command.Post;

namespace PropertyManagement.Api.Endpoints.v1.Property;

public class CreatePropertyEndpoint : Endpoint<CreatePropertyCommand, CreatePropertyResponse>
{
    public override void Configure()
    {
        Options(p => p.WithTags(RouteGroup.Property));
        Tags(RouteGroup.Property);
        Version(1);
        Post("property/post");
        AllowAnonymous();
        Summary(f => f.Summary = "Creating a Property");
    }

    public override async Task HandleAsync(CreatePropertyCommand req, CancellationToken ct)
    {
        var data = await req.ExecuteAsync(ct);
        await SendAsync(data);
    }
}
