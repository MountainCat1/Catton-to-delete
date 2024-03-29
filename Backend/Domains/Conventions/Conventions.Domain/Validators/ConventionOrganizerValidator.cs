﻿using Conventions.Domain.Entities;
using FluentValidation;

namespace Conventions.Domain.Validators;

public class ConventionOrganizerValidator : AbstractValidator<Organizer>
{
    public ConventionOrganizerValidator()
    {
        RuleFor(x => x.AccountId).NotNull().NotEmpty();
        RuleFor(x => x.ConventionId).NotNull().NotEmpty();
    }
}