namespace Management.Domain.Domains.DTO.Students;

public class StudentDTO
{
    public String Id { get; set; }
    public required String FirstName { get; set; }
    public required int Age { get; set; }
}