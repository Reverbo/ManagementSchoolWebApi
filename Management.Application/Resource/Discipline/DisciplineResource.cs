using Management.Infrastructure.Database.Entities;
using Management.Resource.Average;

namespace Management.Resource.Discipline;

public class DisciplineResource
{
    public string? Id { get; set; }
    
    public required string Name { get; set; }

    public required string BimonthlyId { get; set; }
    
    public required string TeacherId {get; set;}
    
    public required List <string> AveragesId {get; set;} 
}