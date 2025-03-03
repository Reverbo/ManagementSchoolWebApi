using FluentValidation;
using Management.Filters.InputFilter.ErrorMessage;
using Management.Filters.InputFilter.Interfaces;
using Management.Resource.Classroom;

namespace Management.Filters.InputFilter.Validators.Classroom;

public class ClassroomValidator : AbstractValidator<ClassroomResource>, IValidatorBase<ClassroomResource>
{
    public ClassroomValidator()
    {
        RuleFor(classroom => classroom.ClassName).NotEmpty().WithMessage(ClassroomErrorMessage.ClassNameEmpty)
            .MaximumLength(50).WithMessage(ClassroomErrorMessage.ClassNameExceededMaximumSize);

        RuleFor(classroom => classroom.SchoolYear).NotEmpty().WithMessage(ClassroomErrorMessage.SchoolYearEmpty);

        RuleForEach(classroom => classroom.StudentsId).NotEmpty().WithMessage(ClassroomErrorMessage.StudentsIdEmpty)
            .Matches("^[a-fA-F0-9]{24}$").WithMessage(ClassroomErrorMessage.StudentsIdIsInvalid);
    }
}