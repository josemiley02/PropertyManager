using System;
using PropertyManagement.Application.Features.Property.Command.Delete;
using PropertyManagement.Common.Dto;

namespace PropertyManagement.Api.Endpoints.v1.Property;

public class DeletePropertyEndpoint : Endpoint<DeletePropertyCommand, Response<NoContentData>>
{
    public override void Configure()
    {
        Options(p => p.WithTags(RouteGroup.Property));
        Tags(RouteGroup.Property);
        Version(1);
        Delete("property/{id}/delete");
        AllowAnonymous();
        Summary(f => f.Summary = "Deleting a Property");
    }

    public override async Task HandleAsync(DeletePropertyCommand req, CancellationToken ct)
    {
        var data = await req.ExecuteAsync(ct);
        await SendAsync(data);
    }
}
