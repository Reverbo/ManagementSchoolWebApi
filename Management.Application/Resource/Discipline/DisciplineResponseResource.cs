using Management.Resource.Average;

namespace Management.Resource.Discipline;

public class DisciplineResponseResource
{
    public string? Id { get; set; }
    
    public required string Name { get; set; }

    public required string BimonthlyId { get; set; }
    
    public required string TeacherId {get; set;}
    
    public List<AverageResource> Averages {get; set;}
}