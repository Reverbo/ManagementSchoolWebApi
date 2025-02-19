namespace Management.Domain.Domains.Exceptions;

public class StudentDocumentException : StudentException
{
    public StudentDocumentException() : base("Student exception error.")
    {
    }

    public StudentDocumentException(string message) : base(message)
    {
    }
    
    public StudentDocumentException(int statusCode, string message) : base(message)
    {
        StatusCode = statusCode;
    }
}