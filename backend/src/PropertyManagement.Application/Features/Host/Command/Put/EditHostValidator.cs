using System;
using FluentValidation;

namespace PropertyManagement.Application.Features.Host.Command.Put;

public class EditHostValidator : CoreValidator<EditHostCommand>
{
    public EditHostValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("The Host Id is required")
            .GreaterThan(0)
            .WithMessage("The Host Id must be positive");
            
        RuleFor(x => x.FullName)
            .NotEmpty()
            .WithMessage("The Host Name is required");

        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("The Host Email is required");
        
        RuleFor(x => x.Phone)
            .NotEmpty()
            .WithMessage("The Host Phone is required");
    }
}
