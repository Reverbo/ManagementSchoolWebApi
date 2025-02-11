namespace Management.Resource.Discipline;

public class DisciplineCreateResource
{
    public string? Id { get; set; }
    
    public required string Name { get; set; }

    public required string BimonthlyId { get; set; }
    
    public required string TeacherId {get; set;}
}