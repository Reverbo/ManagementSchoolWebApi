namespace Management.Resource.Teachers;

public class TeacherResource
{
    public string? Id { get; set; }

    public required string? FullName { get; set; }

    public required string? DateBirth { get; set; }
    
    public required string? Cpf {get; set;}
    
    public required string? TeacherContact { get; set; }
    
    public required string? DisciplineId { get; set; }

    public required string? ClassroomId { get; set; }

    public required decimal? Salary { get; set; }
}