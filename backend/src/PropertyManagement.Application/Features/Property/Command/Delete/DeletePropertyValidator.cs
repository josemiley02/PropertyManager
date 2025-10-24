using System;
using FluentValidation;

namespace PropertyManagement.Application.Features.Property.Command.Delete;

public class DeletePropertyValidator : CoreValidator<DeletePropertyCommand>
{
    public DeletePropertyValidator()
    {
        RuleFor(p => p.Id)
            .NotEmpty()
            .WithMessage("The Id is required")
            .GreaterThan(0)
            .WithMessage("The Id must be positive");
    }
}
