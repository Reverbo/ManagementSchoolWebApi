using Management.Domain.Domains.DTO.Discipline;

namespace Management.Domain.Domains.DTO.Bimonthly;

public class BimonthlyResponseDTO
{
    public string Id { get; set; }
    public required string StartDate { get; set; }
    public required string EndDate { get; set; }
    public required string ClassroomId { get; set; }
    public required List<DisciplineDTO> Disciplines { get; set; }
}