using FluentValidation;
using Management.Filters.InputFilter.ErrorMessage;
using Management.Filters.InputFilter.Interfaces;
using Management.Resource.Student;

namespace Management.Filters.InputFilter.Validators.Student;

public class StudentUpdateValidator : AbstractValidator<StudentUpdateResource>, IValidatorBase<StudentUpdateResource>
{
    public StudentUpdateValidator()
    {
        RuleFor(student => student.FullName).NotEmpty().WithMessage(StudentErrorMessage.FullNameEmpty)
            .MaximumLength(100).WithMessage(StudentErrorMessage.FullNameExceededMaximumSize);
    
        RuleFor(student => student.SocialName).NotEmpty().WithMessage(StudentErrorMessage.SocialNameEmpty)
            .MaximumLength(100).WithMessage(StudentErrorMessage.SocialNameExceededMaximumSize);
    
        RuleFor(student => student.FatherName).NotEmpty().WithMessage(StudentErrorMessage.FatherNameEmpty)
            .MaximumLength(100).WithMessage(StudentErrorMessage.FatherNameExceededMaximumSize);
    
        RuleFor(student => student.MotherName).NotEmpty().WithMessage(StudentErrorMessage.MotherNameEmpty)
            .MaximumLength(100).WithMessage(StudentErrorMessage.MotherNameExceededMaximumSize);

        RuleFor(student => student.ClassroomId).NotEmpty().WithMessage(StudentErrorMessage.ClassroomIdEmpty)
            .Matches("^[a-fA-F0-9]{24}$").WithMessage(StudentErrorMessage.ClassroomIdIsInvalid);
    }
}
