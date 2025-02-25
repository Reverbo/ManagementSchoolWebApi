using FluentValidation;
using Management.Filters.InputFilter.ErrorMessage;
using Management.Filters.InputFilter.Interfaces;
using Management.Resource.Average;

namespace Management.Filters.InputFilter.Validators.Average;

public class AverageValidator : AbstractValidator<AverageResource>, IValidatorBase<AverageResource>
{
    public AverageValidator()
    {
        RuleFor(average => average.DisciplineId)
            .NotEmpty()
            .WithMessage(AverageErrorMessage.DisciplineIdEmpty);
        
        RuleFor(average => average.Total)
            .NotEmpty()
            .WithMessage(AverageErrorMessage.DisciplineIdEmpty);
    }
}