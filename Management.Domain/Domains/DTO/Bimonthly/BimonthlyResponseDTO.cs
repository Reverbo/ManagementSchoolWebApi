using Management.Domain.Domains.DTO.Discipline;

namespace Management.Domain.Domains.DTO.Bimonthly;

public class BimonthlyResponseDTO
{
    public required string Id { get; set; }
    public required string StartDate { get; set; }
    public required string EndDate { get; set; }
    public required string ClassroomId { get; set; }
    public required List<DisciplineResponseDTO> Disciplines { get; set; }
}