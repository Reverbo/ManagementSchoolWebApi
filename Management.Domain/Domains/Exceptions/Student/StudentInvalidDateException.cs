namespace Management.Domain.Domains.Exceptions;

public class StudentInvalidDateException : StudentException
{
    public StudentInvalidDateException() : base("Student exception error.")
    {
    }

    public StudentInvalidDateException(string message) : base(message)
    {
    }
    
    public StudentInvalidDateException(int statusCode, string message) : base(message)
    {
        StatusCode = statusCode;
    }
}