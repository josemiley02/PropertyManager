using System;
using FluentValidation;

namespace PropertyManagement.Application.Features.Booking.Command.Post;

public class CreateBookingValidator : CoreValidator<CreateBookingCommand>
{
    public CreateBookingValidator()
    {
        RuleFor(b => b.PropertyId)
            .NotEmpty()
            .WithMessage("Property Id is required")
            .GreaterThan(0)
            .WithMessage("The Property Id must be positive");

        RuleFor(b => b.CheckIn)
            .NotEmpty()
            .WithMessage("The date for CheckIn is required")
            .GreaterThanOrEqualTo(DateTime.Now)
            .WithMessage("The date for CheckIn cannot be in the past");
        
        RuleFor(b => b.CheckOut)
            .NotEmpty()
            .WithMessage("The date for CheckOut is required")
            .GreaterThan(DateTime.Now)
            .WithMessage("The date for CheckOut cannot be in the past");
    }
}
