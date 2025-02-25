namespace Management.Domain.Domains.Exceptions;

public class ClassroomNotFoundException : ClassroomException
{
    public ClassroomNotFoundException() : base("Classroom exception error")
    {
    }

    public ClassroomNotFoundException(string message) : base(message)
    {
    }

    public ClassroomNotFoundException(int statusCode, string message) : base(message)
    {
        StatusCode = statusCode;
    }
}