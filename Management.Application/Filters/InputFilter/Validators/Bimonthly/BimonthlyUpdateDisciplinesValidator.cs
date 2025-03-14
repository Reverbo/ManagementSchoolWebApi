using System.Text.RegularExpressions;
using FluentValidation;
using Management.Filters.InputFilter.ErrorMessage;
using Management.Filters.InputFilter.Interfaces;
using Management.Resource.Bimonthly;

namespace Management.Filters.InputFilter.Validators.Bimonthly;

public class BimonthlyUpdateDisciplinesValidator : AbstractValidator<BimonthlyUpdateDisciplinesResource>,
    IValidatorBase<BimonthlyUpdateDisciplinesResource>
{
    public BimonthlyUpdateDisciplinesValidator()
    {
        RuleForEach(bimonthly => bimonthly.DisciplinesId)
            .NotEmpty()
            .WithMessage(BimonthlyErrorMessage.DisciplinesIdEmpty)
            .Matches("^[a-fA-F0-9]{24}$").WithMessage(BimonthlyErrorMessage.DisciplinesIdInvalid);

    }
}