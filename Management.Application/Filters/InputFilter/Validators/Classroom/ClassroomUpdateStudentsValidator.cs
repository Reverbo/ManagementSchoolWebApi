using FluentValidation;
using Management.Filters.InputFilter.ErrorMessage;
using Management.Filters.InputFilter.Interfaces;
using Management.Resource.Classroom;

namespace Management.Filters.InputFilter.Validators.Classroom;

public class ClassroomUpdateStudentsValidator : AbstractValidator<ClassroomUpdateStudentsResource>, IValidatorBase<ClassroomUpdateStudentsResource>
{
    public ClassroomUpdateStudentsValidator()
    {
        RuleForEach(classroom => classroom.StudentsId)
            .NotEmpty()
            .WithMessage(ClassroomErrorMessage.StudentsIdEmpty)
            .Matches("^[a-fA-F0-9]{24}$").WithMessage(ClassroomErrorMessage.StudentsIdIsInvalid);
    }
}