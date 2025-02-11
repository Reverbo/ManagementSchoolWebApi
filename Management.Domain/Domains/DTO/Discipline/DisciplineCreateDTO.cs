namespace Management.Domain.Domains.DTO.Discipline;

public class DisciplineCreateDTO
{
    public string? Id { get; set; }
    
    public required string Name { get; set; }

    public required string BimonthlyId { get; set; }
    
    public string TeacherId {get; set;}
}