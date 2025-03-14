using FluentValidation;
using Management.Filters.InputFilter.ErrorMessage;
using Management.Filters.InputFilter.Interfaces;
using Management.Resource.Bimonthly;

namespace Management.Filters.InputFilter.Validators.Bimonthly;

public class BimonthlyValidator : AbstractValidator<BimonthlyCreateResource>, IValidatorBase<BimonthlyCreateResource>
{
    public BimonthlyValidator()
    {
        RuleFor(bimonthly => bimonthly.StartDate).NotEmpty().WithMessage(BimonthlyErrorMessage.StartDateEmpty);

        RuleFor(bimonthly => bimonthly.EndDate).NotEmpty().WithMessage(BimonthlyErrorMessage.EndDateEmpty);

        RuleFor(bimonthly => bimonthly.ClassroomId).NotEmpty().WithMessage(BimonthlyErrorMessage.ClassroomIdEmpty)
            .Matches("^[a-fA-F0-9]{24}$").WithMessage(BimonthlyErrorMessage.ClassroomIdInvalid);
    }
}