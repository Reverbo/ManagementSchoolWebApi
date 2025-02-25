namespace Management.Domain.Domains.Exceptions.Teacher;

public class TeacherInvalidDateException : TeacherException
{
    public TeacherInvalidDateException() : base("Teacher exception error")
    {
    }

    public TeacherInvalidDateException(string message) : base(message)
    {
    }

    public TeacherInvalidDateException(int statusCode, string message) : base(message)
    {
        StatusCode = statusCode;
    }
}