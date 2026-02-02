using FluentValidation;
using Hardship.Api.Models.Domain;

public class HardshipApplicationValidator
    : AbstractValidator<HardshipApplication>
{
    public HardshipApplicationValidator()
    {
        RuleFor(x => x.CustomerName)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.Income)
            .GreaterThan(0);

        RuleFor(x => x.Expenses)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.DateOfBirth)
            .Must(dob => dob.Date < DateTime.Today)
            .WithMessage("Date of birth must be in the past.");
    }

}
