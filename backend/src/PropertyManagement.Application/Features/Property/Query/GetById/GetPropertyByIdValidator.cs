using System;
using FluentValidation;

namespace PropertyManagement.Application.Features.Property.Query.GetById;

public class GetPropertyByIdValidator : CoreValidator<GetPropertyByIdCommand>
{
    public GetPropertyByIdValidator()
    {
        RuleFor(p => p.Id)
            .NotEmpty()
            .WithMessage("The Id is required")
            .GreaterThan(0)
            .WithMessage("The Id must be positive");
    }
}
