namespace Management.Domain.Domains.Exceptions;

public class DisciplineException : BaseManagementSchoolException
{
    public DisciplineException() : base("Discipline exception error.")
    {
    }

    public DisciplineException(string message) : base(message)
    {
    }
    
    public DisciplineException(int statusCode, string message) : base(message)
    {
        StatusCode = statusCode;
    }
}