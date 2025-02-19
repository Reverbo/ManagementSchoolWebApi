namespace Management.Domain.Domains.Exceptions;

public class AverageAlreadyException : AverageException
{
    public AverageAlreadyException() : base("Average exception error.")
    {
    }

    public AverageAlreadyException(string message) : base(message)
    {
    }
    
    public AverageAlreadyException(int statusCode, string message) : base(message)
    {
        StatusCode = statusCode;
    }
}