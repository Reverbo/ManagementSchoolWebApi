namespace Management.Domain.Domains.DTO.Average;

public class AverageDTO
{
    public string Id { get; set; }
    public string DisciplineId { get; set; }
    public string StudentId { get; set; }
    public string Total { get; set; }
    public ScoresDTO Scores { get; set; }
}