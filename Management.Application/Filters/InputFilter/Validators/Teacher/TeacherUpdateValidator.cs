using FluentValidation;
using Management.Filters.InputFilter.ErrorMessage;
using Management.Filters.InputFilter.Interfaces;
using Management.Resource.Teachers;
namespace Management.Filters.InputFilter.Validators.Teacher;

public class TeacherUpdateValidator : AbstractValidator<TeacherUpdateResource>, IValidatorBase<TeacherUpdateResource>
{
    public TeacherUpdateValidator()
    {
        RuleFor(teacher => teacher.FullName).NotEmpty().WithMessage(TeacherErrorMessage.FullNameEmpty)
            .MaximumLength(100).WithMessage(TeacherErrorMessage.NameExceededMaximumSize);

        RuleFor(teacher => teacher.DisciplineId).NotEmpty().WithMessage(TeacherErrorMessage.DisciplineIdEmpty)
            .Matches("^[a-fA-F0-9]{24}$").WithMessage(TeacherErrorMessage.DisciplineIdIsInvalid);
        
        RuleFor(teacher => teacher.ClassroomId).NotEmpty().WithMessage(TeacherErrorMessage.ClassroomIdEmpty)
            .Matches("^[a-fA-F0-9]{24}$").WithMessage(TeacherErrorMessage.ClassroomIdIsInvalid);
        
        RuleFor(teacher => teacher.TeacherContact).NotEmpty().WithMessage(TeacherErrorMessage.TeacherContactEmpty);

        RuleFor(teacher => teacher.Salary).NotEmpty().WithMessage(TeacherErrorMessage.SalaryEmpty);
        
    }
}