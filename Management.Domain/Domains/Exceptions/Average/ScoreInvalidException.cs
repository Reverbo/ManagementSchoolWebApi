namespace Management.Domain.Domains.Exceptions;

public class ScoreInvalidException : AverageException
{
    public ScoreInvalidException() : base("Score exception error.")
    {
    }

    public ScoreInvalidException(string message) : base(message)
    {
    }
    
    public ScoreInvalidException(int statusCode, string message) : base(message)
    {
        StatusCode = statusCode;
    }
}