using FluentValidation;
using Management.Filters.InputFilter.ErrorMessage;
using Management.Filters.InputFilter.Interfaces;
using Management.Resource.Average;

namespace Management.Filters.InputFilter.Validators.Average;

public class ScoreValidator : AbstractValidator<ScoresResource>, IValidatorBase<ScoresResource>
{
    public ScoreValidator()
    {
        RuleFor(score => score.FirstScore)
            .NotEmpty()
            .WithMessage(AverageErrorMessage.FirstScoreEmpty)
            .LessThanOrEqualTo(10).WithMessage(AverageErrorMessage.FirstScoreScoreExceededMaximumValue);

        RuleFor(score => score.SecondScore)
            .NotEmpty()
            .WithMessage(AverageErrorMessage.SecondScoreEmpty)
            .LessThanOrEqualTo(10).WithMessage(AverageErrorMessage.SecondScoreExceededMaximumValue);
    }
}

