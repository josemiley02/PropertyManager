namespace PropertyManagement.Application.Features.Property.Query.GetById;

public record GetPropertyByIdCommand : ICommand<GetPropertyByIdResponse>
{
    public long Id { get; init; }
}
