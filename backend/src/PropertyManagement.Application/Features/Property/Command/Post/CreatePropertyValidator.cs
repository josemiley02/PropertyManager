using System;
using FluentValidation;

namespace PropertyManagement.Application.Features.Property.Command.Post;

public class CreatePropertyValidator : CoreValidator<CreatePropertyCommand>
{
    public CreatePropertyValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithErrorCode("The Property Name is required");

        RuleFor(x => x.Address)
            .NotEmpty()
            .WithErrorCode("The Property Address is required");

        RuleFor(x => x.PricePerNight)
            .NotEmpty()
            .WithErrorCode("The Property Price is required")
            .GreaterThan(0)
            .WithMessage("The Property cannot be free");

        RuleFor(x => x.HostId)
            .NotEmpty()
            .WithErrorCode("The Host Id is required")
            .GreaterThan(0)
            .WithErrorCode("The Host Id must be positive");
    }
}
