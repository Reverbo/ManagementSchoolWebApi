namespace Management.Resource.Student;

public class ClassResouce
{
    public String? Id { get; set; }
    
    public required string ClassName { get; set; }
    
    public List<StudentResource> Students { get; set; }
    
    public required string TeachersClass { get; set; }

    public required DateTime TimeClassrom { get; set; }

    public required int StudentCapacity { get; set; }
    
    
}