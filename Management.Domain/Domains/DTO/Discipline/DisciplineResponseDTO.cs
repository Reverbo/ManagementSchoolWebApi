using Management.Domain.Domains.DTO.Average;

namespace Management.Domain.Domains.DTO.Discipline;

public class DisciplineResponseDTO
{
    public string? Id { get; set; }
    
    public required string Name { get; set; }

    public required string BimonthlyId { get; set; }
    
    public required string TeacherId {get; set;}

    public required List<AverageDTO>? Averages { get; set; }
}