using FluentValidation;
using Management.Filters.InputFilter.ErrorMessage;
using Management.Filters.InputFilter.Interfaces;
using Management.Resource.Average;

namespace Management.Filters.InputFilter.Validators.Average;

public class AverageValidator : AbstractValidator<AverageCreateResource>, IValidatorBase<AverageCreateResource>
{
    public AverageValidator()
    {
        RuleFor(average => average.DisciplineId)
            .Matches("^[a-fA-F0-9]{24}$").WithMessage(AverageErrorMessage.StudentIdInvalid)
            .NotEmpty()
            .WithMessage(AverageErrorMessage.DisciplineIdEmpty)
            .Matches("^[a-fA-F0-9]{24}$").WithMessage(AverageErrorMessage.DisciplineIdInvalid);

        RuleFor(average => average.Scores)
            .NotEmpty()
            .WithMessage(AverageErrorMessage.ScoresEmpty);

        RuleFor(average => average.StudentId)
            .Matches("^[a-fA-F0-9]{24}$").WithMessage(AverageErrorMessage.StudentIdInvalid)
            .NotEmpty()
            .WithMessage(AverageErrorMessage.StudentIdEmpty);
    }
}