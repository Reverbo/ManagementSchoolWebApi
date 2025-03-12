using Management.Domain.Domains.DTO.Teachers;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Management.Infrastructure.Database.Entities.Teacher;

public class TeacherEntity : ITeacher
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    [BsonElement("_id")]
    public ObjectId Id { get; set; }

    [BsonRequired] 
    public required string FullName { get; set; }

    [BsonRequired] 
    
    public required string DateBirth { get; set; }

    [BsonRequired]
    public required string Cpf { get; set; }

    [BsonRequired]
    public required string TeacherContact { get; set; }

    [BsonRequired]
    public required string DisciplineId { get; set; }

    [BsonRequired]
    public required string ClassroomId { get; set; }

    [BsonRequired]
    public required decimal Salary { get; set; }

    public void UpdateByTeacherUpdateDto(TeacherUpdateDTO teacherUpdateDto)
    {
        TeacherContact = teacherUpdateDto.TeacherContact;
        ClassroomId = teacherUpdateDto.ClassroomId;
        Salary = teacherUpdateDto.Salary;
        DisciplineId = teacherUpdateDto.DisciplineId;
        FullName = teacherUpdateDto.FullName;
    }
    
}