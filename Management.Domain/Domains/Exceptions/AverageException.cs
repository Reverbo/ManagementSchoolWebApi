namespace Management.Domain.Domains.Exceptions;

public class AverageException : Exception
{
    public int StatusCode { get; set; }

    public AverageException() : base("Average exception error.")
    {
    }

    public AverageException(string message) : base(message)
    {
    }
    
    public AverageException(int statusCode, string message) : base(message)
    {
        StatusCode = statusCode;
    }
}