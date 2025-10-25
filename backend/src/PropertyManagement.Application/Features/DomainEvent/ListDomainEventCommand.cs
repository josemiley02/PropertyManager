using System;
using PropertyManagement.Application.DTOs.Paged;

namespace PropertyManagement.Application.Features.DomainEvent;

public record ListDomainEventCommand : ICommand<IEnumerable<ListDomainEventResponse>>
{

}
