using FluentValidation;
using Management.Filters.InputFilter.ErrorMessage;
using Management.Filters.InputFilter.Interfaces;
using Management.Resource.Classroom;

namespace Management.Filters.InputFilter.Validators.Classroom;

public class ClassroomUpdateValidator : AbstractValidator<ClassroomUpdateResource>, IValidatorBase<ClassroomUpdateResource>
{
    public ClassroomUpdateValidator()
    {
        RuleFor(classroom => classroom.ClassName).NotEmpty().WithMessage(ClassroomErrorMessage.ClassNameEmpty)
            .MaximumLength(50).WithMessage(ClassroomErrorMessage.ClassNameExceededMaximumSize);

        RuleFor(classroom => classroom.SchoolYear).NotEmpty().WithMessage(ClassroomErrorMessage.SchoolYearEmpty);
    }
}