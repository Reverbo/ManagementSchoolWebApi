namespace Management.Domain.Domains.DTO.Bimonthly;

public class BimonthlyCreateDTO
{
    public required string StartDate { get; set; }
    public required string EndDate { get; set; }
    public required string ClassroomId { get; set; }
}