using System;
using PropertyManagement.Application.Features.Property.Command.Put;

namespace PropertyManagement.Api.Endpoints.v1.Property;

public class EditPropertyEndpoint : Endpoint<EditPropertyCommad, EditPropertyResponse>
{
    public override void Configure()
    {
        Options(p => p.WithTags(RouteGroup.Property));
        Tags(RouteGroup.Property);
        Version(1);
        Put("property/put");
        AllowAnonymous();
        Summary(f => f.Summary = "Editing a Property");
    }
    public override async Task HandleAsync(EditPropertyCommad req, CancellationToken ct)
    {
        var data = await req.ExecuteAsync();
        await SendAsync(data);
    }
}
