namespace Management.Domain.Domains.Exceptions;

public class StudentNotFoundException : BaseManagementSchoolException
{
    public StudentNotFoundException() : base("Average exception error.")
    {
    }

    public StudentNotFoundException(string message) : base(message)
    {
    }
    
    public StudentNotFoundException(int statusCode, string message) : base(message)
    {
        StatusCode = statusCode;
    }
}