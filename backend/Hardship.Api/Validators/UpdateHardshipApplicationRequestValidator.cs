using FluentValidation;
using Hardship.Api.Models.DTOs;

public class UpdateHardshipApplicationRequestValidator
    : AbstractValidator<UpdateHardshipApplicationRequest>
{
    public UpdateHardshipApplicationRequestValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0);

        RuleFor(x => x.CustomerName)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.DateOfBirth)
            .LessThan(DateTime.Today);

        RuleFor(x => x.Income)
            .GreaterThan(0);

        RuleFor(x => x.Expenses)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.HardshipReason)
            .MaximumLength(500)
            .When(x => !string.IsNullOrWhiteSpace(x.HardshipReason));

    }
}
