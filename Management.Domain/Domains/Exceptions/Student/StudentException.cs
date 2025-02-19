namespace Management.Domain.Domains.Exceptions;

public class StudentException : BaseManagementSchoolException
{
    public StudentException()  : base("Student exception error") {}
    
    public StudentException(string message) : base(message) {}

    public StudentException(int statusCode, string message) : base(message)
    {
        StatusCode = statusCode;
    }
}