namespace Management.Domain.Domains.DTO.Classroom;

public class ClassroomDTO
{
    public string? Id { get; set; }
    
    public required string ClassName { get; set; }
    
    public required string SchoolYear { get; set; }
    
    public required List<string> StudentsId { get; set; }
}