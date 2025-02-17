namespace Management.Domain.Domains.Exceptions;

public class BaseManagementSchoolException : Exception
{
    public int StatusCode { get; set; }
    
    public BaseManagementSchoolException() : base("BaseManagementSchool exception error.")
    {
    }

    public BaseManagementSchoolException(string message) : base(message)
    {
    }
    
    public BaseManagementSchoolException(int statusCode, string message) : base(message)
    {
        StatusCode = statusCode;
    }
}