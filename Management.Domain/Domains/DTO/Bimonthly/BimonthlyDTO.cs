namespace Management.Domain.Domains.DTO.Bimonthly;

public class BimonthlyDTO
{
    public string? Id { get; set; }
    public string StartDate { get; set; }
    public string EndDate { get; set; }
    public string ClassroomId { get; set; }
    public required List<string> DisciplinesId { get; set; }
}