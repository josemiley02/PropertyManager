using System;
using FluentValidation;

namespace PropertyManagement.Application;

public class CoreValidator<TCommand> : Validator<TCommand> where TCommand : class
{
    public CoreValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Stop;
    }
}