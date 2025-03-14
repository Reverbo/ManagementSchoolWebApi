using FluentValidation;
using Management.Filters.InputFilter.ErrorMessage;
using Management.Filters.InputFilter.Interfaces;
using Management.Resource.Bimonthly;

namespace Management.Filters.InputFilter.Validators.Bimonthly;

public class BimonthlyUpdateValidator : AbstractValidator<BimonthlyDatesResource>, IValidatorBase<BimonthlyDatesResource>
{
    public BimonthlyUpdateValidator()
    {
        RuleFor(bimonthly => bimonthly.StartDate).NotEmpty().WithMessage(BimonthlyErrorMessage.StartDateEmpty);
        
        RuleFor(bimonthly => bimonthly.EndDate).NotEmpty().WithMessage(BimonthlyErrorMessage.EndDateEmpty);
    }
}