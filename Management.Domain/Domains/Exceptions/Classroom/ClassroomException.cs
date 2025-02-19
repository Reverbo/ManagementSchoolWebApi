namespace Management.Domain.Domains.Exceptions;

public class ClassroomException : BaseManagementSchoolException
{
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