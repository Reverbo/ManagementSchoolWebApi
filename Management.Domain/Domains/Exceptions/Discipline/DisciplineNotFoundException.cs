namespace Management.Domain.Domains.Exceptions;

public class DisciplineNotFoundException : DisciplineException
{
    public DisciplineNotFoundException() : base("Discipline exception error.")
    {
    }

    public DisciplineNotFoundException(string message) : base(message)
    {
    }
    
    public DisciplineNotFoundException(int statusCode, string message) : base(message)
    {
        StatusCode = statusCode;
    }
}