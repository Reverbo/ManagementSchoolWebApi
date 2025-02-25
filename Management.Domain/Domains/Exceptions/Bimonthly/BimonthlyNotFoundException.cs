namespace Management.Domain.Domains.Exceptions.Bimonthly;

public class BimonthlyNotFoundException : BimonthlyException
{
    public BimonthlyNotFoundException() : base("Bimonthly exception error.")
    {
    }

    public BimonthlyNotFoundException(string message) : base(message)
    {
    }
    
    public BimonthlyNotFoundException(int statusCode, string message) : base(message)
    {
        StatusCode = statusCode;
    }
}