namespace Management.Resource.Average;

public class AverageResource
{
    public string? Id { get; set; }
    public string? DisciplineId { get; set; }
    public string? StudentId { get; set; }
    public string? Total { get; set; }
    public ScoresResource? Scores { get; set; }
}