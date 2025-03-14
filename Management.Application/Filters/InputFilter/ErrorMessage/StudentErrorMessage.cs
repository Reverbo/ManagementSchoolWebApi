namespace Management.Filters.InputFilter.ErrorMessage;

public static class StudentErrorMessage
{
    public const string FullNameEmpty = "The 'FullName' field is cannot be empty.";

    public const string FullNameExceededMaximumSize = "the 'FullName' field cannot exceed {0} characters";

    public const string SocialNameEmpty = "The 'SocialName' field is cannot be empty.";

    public const string SocialNameExceededMaximumSize = "the 'SocialName' field cannot exceed {0} characters";
    
    
    public const string FatherNameEmpty = "The 'FatherName' field is cannot be empty.";

    public const string FatherNameExceededMaximumSize = "the 'FatherName' field cannot exceed {0} characters";

    public const string MotherNameEmpty = "The 'MotherName' field is cannot be empty.";

    public const string MotherNameExceededMaximumSize = "the 'FatherName' field cannot exceed {0} characters";
    
    
    public const string DateBirthEmpty = "The 'DateOfBirth' field is cannot be empty.";
    
    
    public const string CpfEmpty = "The 'CPF' field is cannot be empty.";

    public const string RgEmpty = "The 'RG' field is cannot be empty.";

    public const string InvalidCpf = "invalid CPF";

    public const string InvalidRg = "invalid RG";
    

    public const string ClassroomIdEmpty = "The 'ClassroomId' field is cannot be empty.";
    public const string ClassroomIdIsInvalid = "Invalid is ClassroomId";

    public const string EmailEmpty = "The 'Email' field is cannot be empty.";

    public const string InvalidEmail = "Invalid email address";
}