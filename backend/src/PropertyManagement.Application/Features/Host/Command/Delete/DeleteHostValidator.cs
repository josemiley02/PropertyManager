using FluentValidation;

namespace PropertyManagement.Application.Features.Host.Command.Delete;

public class DeleteHostValidator : CoreValidator<DeleteHostCommand>
{
    public DeleteHostValidator()
    {
        RuleFor(h => h.Id)
            .NotEmpty()
            .WithMessage("The Host Id is required")
            .GreaterThan(0)
            .WithMessage("The Host Id must be positive");
    }
}
