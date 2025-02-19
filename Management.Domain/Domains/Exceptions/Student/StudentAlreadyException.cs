namespace Management.Domain.Domains.Exceptions;

public class StudentAlreadyException : StudentException
{
    public StudentAlreadyException()  : base("Student exception error") {}
    
    public StudentAlreadyException(string message) : base(message) {}

    public StudentAlreadyException(int statusCode, string message) : base(message)
    {
        StatusCode = statusCode;
    }
}