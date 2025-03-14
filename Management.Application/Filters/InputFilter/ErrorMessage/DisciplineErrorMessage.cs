namespace Management.Filters.InputFilter.ErrorMessage;

public abstract class DisciplineErrorMessage
{
    public const string NameEmpty = "The 'Name' field is cannot be empty.";
    public const string NameExceededMaximumSize = "the name field cannot exceed {0} characters";
        
    public const string BimonthlyIdEmpty = "The 'BimonthlyId' field is cannot be empty.";
    public const string BimonthlyIdIsInvalid = "Invalid is BimonthlyId";
    
    public const string TeacherIdEmpty = "The 'DisciplineId' field is cannot be empty.";
    public const string TeacherIdIsInvalid = "Invalid is TeacherId";
    
    public const string AveragesIdEmpty = "The 'AveragesId' field is cannot be empty.";
    public const string AveragesIdIsInvalid = "Invalid is AveragesId";
}