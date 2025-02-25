namespace Management.Domain.Domains.Exceptions.Teacher;

public class TeacherNotFoundException : TeacherException
{
    public TeacherNotFoundException() : base("Teacher exception error")
    {
    }

    public TeacherNotFoundException(string message) : base(message)
    {
    }

    public TeacherNotFoundException(int statusCode, string message) : base(message)
    {
        StatusCode = statusCode;
    }
}