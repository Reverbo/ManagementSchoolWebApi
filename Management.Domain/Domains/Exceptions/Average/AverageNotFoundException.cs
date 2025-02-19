namespace Management.Domain.Domains.Exceptions;

public class AverageNotFoundException : AverageException
{
    public AverageNotFoundException() : base("Average exception error.")
    {
    }

    public AverageNotFoundException(string message) : base(message)
    {
    }
    
    public AverageNotFoundException(int statusCode, string message) : base(message)
    {
        StatusCode = statusCode;
    }
}