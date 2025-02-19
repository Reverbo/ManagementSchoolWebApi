namespace Management.Domain.Domains.Exceptions.Bimonthly;

public class BimonthlyInvalidDateException : BimonthlyException
{
    public BimonthlyInvalidDateException() : base("Bimonthly exception error.")
    {
    }
    
    public BimonthlyInvalidDateException(string message) : base(message)
    {
    }

    public BimonthlyInvalidDateException(int statusCode, string message) : base(message)
    {
        StatusCode = statusCode;
    }
}