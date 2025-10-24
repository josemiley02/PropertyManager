using System;
using FluentValidation;

namespace PropertyManagement.Application.Features.Property.Command.Put;

public class EditPropertyValidator : CoreValidator<EditPropertyCommad>
{
    public EditPropertyValidator()
    {
        RuleFor(p => p.Id)
            .NotEmpty()
            .WithErrorCode("The Id is required")
            .GreaterThan(0)
            .WithMessage("The Id must be positive");
        
        RuleFor(x => x.Address)
            .NotEmpty()
            .WithErrorCode("The Property Address is required");

        RuleFor(x => x.PricePerNight)
            .NotEmpty()
            .WithErrorCode("The Property Price is required")
            .GreaterThan(0)
            .WithMessage("The Property cannot be free");
    }
}
