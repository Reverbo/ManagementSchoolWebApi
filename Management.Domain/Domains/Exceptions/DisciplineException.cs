namespace Management.Domain.Domains.Exceptions;

public class DisciplineException : Exception
{
    public int StatusCode { get; set; }

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