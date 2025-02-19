namespace Management.Domain.Domains.Exceptions;

public class DisciplineNotFoundForStudentException : DisciplineException
{
    public DisciplineNotFoundForStudentException() : base("Discipline exception error.")
    {
    }

    public DisciplineNotFoundForStudentException(string message) : base(message)
    {
    }
    
    public DisciplineNotFoundForStudentException(int statusCode, string message) : base(message)
    {
        StatusCode = statusCode;
    }
    
}