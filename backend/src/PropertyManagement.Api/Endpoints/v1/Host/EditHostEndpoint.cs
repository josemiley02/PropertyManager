using System;
using PropertyManagement.Application.Features.Host.Command.Put;

namespace PropertyManagement.Api.Endpoints.v1.Host;

public class EditHostEndpoint : Endpoint<EditHostCommand, EditHostResponse>
{
    public override void Configure()
    {
        Options(h => h.WithTags(RouteGroup.Host));
        Tags(RouteGroup.Host);
        Version(1);
        Put("host/{id}/put");
        AllowAnonymous();
        Summary(f => f.Summary = "Editing a Host");
    }
    public override async Task HandleAsync(EditHostCommand req, CancellationToken ct)
    {
        var data = await req.ExecuteAsync(ct);
        await SendAsync(data);
    }
}
