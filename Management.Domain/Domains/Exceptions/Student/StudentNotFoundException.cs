namespace Management.Domain.Domains.Exceptions;

public class StudentNotFoundException : StudentException
{
    public StudentNotFoundException() : base("Student exception error.")
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