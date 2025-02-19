namespace Management.Domain.Domains.Exceptions;

public class DisciplineAlreadyException : DisciplineException
{
    public DisciplineAlreadyException() : base("Discipline exception error.")
    {
    }

    public DisciplineAlreadyException(string message) : base(message)
    {
    }
    
    public DisciplineAlreadyException(int statusCode, string message) : base(message)
    {
        StatusCode = statusCode;
    }
}