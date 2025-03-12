using Management.Domain.Domains.DTO.Classroom;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Management.Infrastructure.Database.Entities;

public class ClassroomEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    [BsonElement("_id")]
    
    public ObjectId Id { get; set; }
    
    [BsonRequired]
    public required string ClassName { get; set; } 
    
    [BsonRequired]
    public required string SchoolYear { get; set; } 
    
    [BsonRequired]
    public required List<string> StudentsId { get; set; }
    
    public void UpdateByClassroomDto(ClassroomUpdateDTO classroomUpdateDto)
    { 
        ClassName = classroomUpdateDto.ClassName;
        SchoolYear = classroomUpdateDto.SchoolYear;
    }
}