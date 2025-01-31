namespace Management.Domain.Domains.Exceptions;

public class ClassroomException : Exception
{
    public int StatusCode { get; set; }

    public ClassroomException() : base("Classroom exception error")
    {
    }

    public ClassroomException(string message) : base(message)
    {
    }

    public ClassroomException(int statusCode, string message) : base(message)
    {
        StatusCode = statusCode;
    }
    
}