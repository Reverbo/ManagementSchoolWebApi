using Management.Domain.Domains.DTO.Students;
using Management.Domain.Domains.DTO.Teachers;
using MongoDB.Bson;

namespace Management.Domain.Domains.DTO.Classroom;

public class ClassroomResponseDTO
{
    public string Id { get; set; }
    public required string ClassName { get; set; }
    
    public required string SchoolYear { get; set; }
    
    public required string BimonthlyId { get; set; }
   
    public required List<StudentDTO> Students { get; set; }
    
}