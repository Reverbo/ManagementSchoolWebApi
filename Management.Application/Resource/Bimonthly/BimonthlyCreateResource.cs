namespace Management.Resource.Bimonthly;

public class BimonthlyCreateResource
{
    public required string? StartDate { get; set; }
    public required string? EndDate { get; set; }
    public required string? ClassroomId { get; set; }
}