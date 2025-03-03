using FluentValidation;
using Management.Filters.InputFilter.ErrorMessage;
using Management.Filters.InputFilter.Interfaces;
using Management.Resource.Student;

namespace Management.Filters.InputFilter.Validators.Student;

public class StudentValidator : AbstractValidator<StudentResource>, IValidatorBase<StudentResource>
{
  public StudentValidator()
  {
    RuleFor(student => student.FullName).NotEmpty().WithMessage(StudentErrorMessage.FullNameEmpty)
      .MaximumLength(100).WithMessage(StudentErrorMessage.FullNameExceededMaximumSize);
    
    RuleFor(student => student.SocialName).NotEmpty().WithMessage(StudentErrorMessage.SocialNameEmpty)
      .MaximumLength(100).WithMessage(StudentErrorMessage.SocialNameExceededMaximumSize);
    
    RuleFor(student => student.FatherName).NotEmpty().WithMessage(StudentErrorMessage.FatherNameEmpty)
      .MaximumLength(100).WithMessage(StudentErrorMessage.FatherNameExceededMaximumSize);
    
    RuleFor(student => student.MotherName).NotEmpty().WithMessage(StudentErrorMessage.MotherNameEmpty)
      .MaximumLength(100).WithMessage(StudentErrorMessage.MotherNameExceededMaximumSize);
    
    RuleFor(student => student.Email).NotEmpty().WithMessage(StudentErrorMessage.EmailEmpty)
      .EmailAddress().WithMessage(StudentErrorMessage.InvalidEmail);
    
    RuleFor(student => student.Cpf).NotEmpty().WithMessage(StudentErrorMessage.CpfEmpty)
      .Matches(@"^\d{11}$").WithMessage(StudentErrorMessage.InvalidCpf);
    
    RuleFor(student => student.Rg).NotEmpty().WithMessage(StudentErrorMessage.RgEmpty)
      .Matches(@"^\d{11}$").WithMessage(StudentErrorMessage.InvalidRg);

    RuleFor(student => student.DateBirth).NotEmpty().WithMessage(StudentErrorMessage.DateBirthEmpty);

    RuleFor(student => student.ClassroomId).NotEmpty().WithMessage(StudentErrorMessage.ClassroomIdEmpty)
      .Matches("^[a-fA-F0-9]{24}$").WithMessage(StudentErrorMessage.ClassroomIdIsInvalid);
    
   
  }
}