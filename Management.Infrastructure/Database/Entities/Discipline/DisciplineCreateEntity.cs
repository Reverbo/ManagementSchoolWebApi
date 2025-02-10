namespace Management.Infrastructure.Database.Entities;

public class DisciplineCreateEntity
{
    public string? Id { get; set; }
    
    public required string Name { get; set; }

    public required string BimonthlyId { get; set; }
    
    public required string TeacherId {get; set;}
}