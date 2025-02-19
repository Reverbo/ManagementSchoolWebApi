namespace Management.Domain.Domains.Exceptions;

public class BimonthlyException : BaseManagementSchoolException
{
    public BimonthlyException() : base("Bimonthly exception error.")
    {
    }
    
    public BimonthlyException(string message) : base(message)
    {
    }

    public BimonthlyException(int statusCode, string message) : base(message)
    {
        StatusCode = statusCode;
    }
}