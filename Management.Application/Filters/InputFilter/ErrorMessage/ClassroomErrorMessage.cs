namespace Management.Filters.InputFilter.ErrorMessage;

public static class ClassroomErrorMessage
{
    public const string ClassNameEmpty = "The 'ClassName' field is cannot be empty.";
    
    public const string StudentsIdEmpty = "The 'StudentId' field is cannot be empty.";
    
    public const string StudentsIdIsInvalid = "Invalid is StudentsId";

    public const string ClassNameExceededMaximumSize = "the 'ClassName' field cannot exceed {0} characters";

    public const string SchoolYearEmpty = "The 'SchoolYearEmpty' field is cannot be empty.";
}