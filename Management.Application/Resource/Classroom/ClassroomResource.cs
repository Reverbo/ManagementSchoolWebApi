namespace Management.Resource.Classroom;

public class ClassroomResource
{
    public string? Id { get; set; }
    
    public required string ClassName { get; set; }
    
    public required string SchoolYear { get; set; }
    
    public required string BimonthlyId { get; set; }
   
    public required List<string> StudentsId{ get; set; }
}