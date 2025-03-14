using FluentValidation;
using Management.Filters.InputFilter.ErrorMessage;
using Management.Filters.InputFilter.Interfaces;
using Management.Resource.Discipline;

namespace Management.Filters.InputFilter.Validators.Discipline;

public class DisciplineUpdateAverageValidator : AbstractValidator<DisciplineUpdateAveragesResource>, IValidatorBase<DisciplineUpdateAveragesResource>
{
    public DisciplineUpdateAverageValidator()
    {
        RuleForEach(discipline => discipline.AveragesId).NotEmpty().WithMessage(DisciplineErrorMessage.AveragesIdEmpty)
            .Matches("^[a-fA-F0-9]{24}$").WithMessage(DisciplineErrorMessage.AveragesIdIsInvalid);
    }
}