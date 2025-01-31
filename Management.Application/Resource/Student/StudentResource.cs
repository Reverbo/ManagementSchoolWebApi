namespace Management.Resource.Student;

public class StudentResource
{
    public string? Id { get; set; }
    
    public required string FullName { get; set; }
    
    public required string SocialName { get; set; }
    
    public required int Age { get; set; }
    
    public required string DocumentNumber {get; set;}
    
    public required string DocumentType { get; set; }
    
    public required string Email { get; set; } 
    
    public required string FatherName { get; set; }
    
    public required string MotherName { get; set; }
}