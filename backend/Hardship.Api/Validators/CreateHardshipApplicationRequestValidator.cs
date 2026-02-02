using FluentValidation;
using Hardship.Api.Models.DTOs;

public class CreateHardshipApplicationRequestValidator
    : AbstractValidator<CreateHardshipApplicationRequest>
{
    public CreateHardshipApplicationRequestValidator()
    {
        RuleFor(x => x.CustomerName)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.DateOfBirth)
            .LessThan(DateTime.Today)
            .WithMessage("Date of birth must be in the past.");

        RuleFor(x => x.Income)
            .GreaterThan(0);

        RuleFor(x => x.Expenses)
            .GreaterThanOrEqualTo(0);

       RuleFor(x => x.HardshipReason)
        .MaximumLength(500)
        .When(x => !string.IsNullOrWhiteSpace(x.HardshipReason));

    }
}
