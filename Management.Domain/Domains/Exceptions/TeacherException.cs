namespace Management.Domain.Domains.Exceptions;

public class TeacherException : Exception
{
    public int StatusCode { get; set; }

    public TeacherException() : base("Teacher exception error")
    {
    }

    public TeacherException(string message) : base(message)
    {
    }

    public TeacherException(int statusCode, string message) : base(message)
    {
        StatusCode = statusCode;
    }
}