using System;
using PropertyManagement.Application.Features.Host.Command.Delete;
using PropertyManagement.Common.Dto;

namespace PropertyManagement.Api.Endpoints.v1.Host;

public class DeleteHostEndpoint : Endpoint<DeleteHostCommand, Response<NoContentData>>
{
    public override void Configure()
    {
        Options(h => h.WithTags(RouteGroup.Host));
        Tags(RouteGroup.Host);
        Version(1);
        Delete("/host/delete");
        AllowAnonymous();
        Summary(f => f.Summary = "Deleting a Host");
    }
    public override async Task HandleAsync(DeleteHostCommand req, CancellationToken ct)
    {
        var data = await req.ExecuteAsync(ct);
        await SendAsync(data);
    }
}
