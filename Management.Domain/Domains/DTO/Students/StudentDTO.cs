namespace Management.Domain.Domains.DTO.Students;

public class StudentDTO
{
    public string? Id { get; set; }
    
    public required string FullName { get; set; }
    
    public required string SocialName { get; set; }
    
    public required string DateBirth { get; set; }
    
    public required string ClassroomId { get; set; }
    
    public required string Cpf {get; set;}
    
    public required string Rg { get; set; }
    
    public required string Email { get; set; } 
    
    public required string FatherName { get; set; }
    
    public required string MotherName { get; set; }
    
}