using FluentValidation;
using Management.Filters.InputFilter.ErrorMessage;
using Management.Filters.InputFilter.Interfaces;
using Management.Resource.Discipline;

namespace Management.Filters.InputFilter.Validators.Discipline;

public class DisciplineValidator : AbstractValidator<DisciplineCreateResource>, IValidatorBase<DisciplineCreateResource>
{
    public DisciplineValidator()
    {
        RuleFor(discipline => discipline.Name).NotEmpty().WithMessage(DisciplineErrorMessage.NameEmpty)
            .MaximumLength(50).WithMessage(DisciplineErrorMessage.NameExceededMaximumSize);

        RuleFor(discipline => discipline.BimonthlyId).NotEmpty().WithMessage(DisciplineErrorMessage.BimonthlyIdEmpty)
            .Matches("^[a-fA-F0-9]{24}$").WithMessage(DisciplineErrorMessage.BimonthlyIdIsInvalid);

        RuleFor(discipline => discipline.TeacherId).NotEmpty().WithMessage(DisciplineErrorMessage.TeacherIdEmpty)
            .Matches("^[a-fA-F0-9]{24}$").WithMessage(DisciplineErrorMessage.TeacherIdIsInvalid);

    }
}