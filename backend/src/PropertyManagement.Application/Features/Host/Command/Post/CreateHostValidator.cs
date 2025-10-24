using System;
using FluentValidation;

namespace PropertyManagement.Application.Features.Host.Command.Post;

public class CreateHostValidator : CoreValidator<CreateHostCommand>
{
    public CreateHostValidator()
    {
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
