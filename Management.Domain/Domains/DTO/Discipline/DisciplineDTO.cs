using Management.Domain.Domains.DTO.Average;

namespace Management.Domain.Domains.DTO.Discipline;

public class DisciplineDTO
{
    public string? Id { get; set; }
    
    public required string Name { get; set; }

    public required string BimonthlyId { get; set; }
    
    public required string TeacherId {get; set;}
    
    public required List <string> AveragesId {get; set;} 
}