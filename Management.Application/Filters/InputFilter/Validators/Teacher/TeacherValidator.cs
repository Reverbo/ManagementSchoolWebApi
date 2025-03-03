using FluentValidation;
using Management.Filters.InputFilter.ErrorMessage;
using Management.Filters.InputFilter.Interfaces;
using Management.Resource.Teachers;
using MongoDB.Bson;

namespace Management.Filters.InputFilter.Validators.Teacher;

public class TeacherValidator : AbstractValidator<TeacherResource>, IValidatorBase<TeacherResource>
{
    public TeacherValidator()
    {
        RuleFor(teacher => teacher.FullName).NotEmpty().WithMessage(TeacherErrorMessage.FullNameEmpty)
            .MaximumLength(100).WithMessage(TeacherErrorMessage.NameExceededMaximumSize);

        RuleFor(teacher => teacher.DateBirth).NotEmpty().WithMessage(TeacherErrorMessage.DateBirthEmpty);

        RuleFor(teacher => teacher.DisciplineId).NotEmpty().WithMessage(TeacherErrorMessage.DisciplineIdEmpty)
            .Matches("^[a-fA-F0-9]{24}$").WithMessage(TeacherErrorMessage.DisciplineIdIsInvalid);
        
        RuleFor(teacher => teacher.ClassroomId).NotEmpty().WithMessage(TeacherErrorMessage.ClassroomIdEmpty)
            .Matches("^[a-fA-F0-9]{24}$").WithMessage(TeacherErrorMessage.ClassroomIdIsInvalid);
        
        RuleFor(teacher => teacher.TeacherContact).NotEmpty().WithMessage(TeacherErrorMessage.TeacherContactEmpty);

        RuleFor(teacher => teacher.Salary).NotEmpty().WithMessage(TeacherErrorMessage.SalaryEmpty);
        
        RuleFor(teacher => teacher.Cpf).NotEmpty().WithMessage(TeacherErrorMessage.CpfEmpty)
            .Matches(@"^\d{11}$").WithMessage(TeacherErrorMessage.InvalidCpf);
        
    }
}