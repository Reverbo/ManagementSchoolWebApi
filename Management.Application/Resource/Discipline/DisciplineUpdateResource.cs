namespace Management.Resource.Discipline;

public class DisciplineUpdateResource
{
    public required string Name { get; set; }

    public required string BimonthlyId { get; set; }
    
    public required string TeacherId {get; set;}
}