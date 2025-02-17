namespace Management.Resource.Bimonthly;

public class BimontlhyResource
{
    public string? Id { get; set; }
    public required string StartDate { get; set; }
    public required string EndDate { get; set; }
    public required string ClassroomId { get; set; }
    public required List<string> DisciplinesId { get; set; }
}