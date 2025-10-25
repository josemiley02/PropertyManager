using System;
using PropertyManagement.Application.Features.DomainEvent;

namespace PropertyManagement.Api.Endpoints.v1.DomainEvent;

public class ListDomainEventsEndpoint : EndpointWithoutRequest<IEnumerable<ListDomainEventResponse>>
{
    public override void Configure()
    {
        Options(e => e.WithTags(RouteGroup.DomainEvent));
        Tags(RouteGroup.DomainEvent);
        Version(1);
        Get("domainEvent/get");
        AllowAnonymous();
        Summary(f => f.Summary = "Listing all Domain Events");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var command = new ListDomainEventCommand();
        var data = await command.ExecuteAsync(ct);
        await SendAsync(data);
    }
}
