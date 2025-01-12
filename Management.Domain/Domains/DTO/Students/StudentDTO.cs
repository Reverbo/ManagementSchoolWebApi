namespace Management.Domain.Domains.DTO.Students;

public class StudentDTO
{
    public string Id { get; set; }
    public required string FirstName { get; set; }
    public required int Age { get; set; }
}