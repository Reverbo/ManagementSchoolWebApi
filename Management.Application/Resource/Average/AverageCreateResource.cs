namespace Management.Resource.Average;

public class AverageCreateResource
{
    public string? Id { get; set; }
    public string? DisciplineId { get; set; }
    public string? StudentId { get; set; }
    public ScoresResource? Scores { get; set; }
}