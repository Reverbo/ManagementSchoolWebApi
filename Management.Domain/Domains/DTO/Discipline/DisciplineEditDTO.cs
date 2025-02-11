namespace Management.Domain.Domains.DTO.Discipline;

public class DisciplineEditDTO
{
    public required string Name { get; set; }

    public required string BimonthlyId { get; set; }
    
    public required string TeacherId {get; set;}
}