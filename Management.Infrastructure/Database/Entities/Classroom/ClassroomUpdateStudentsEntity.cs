using MongoDB.Bson.Serialization.Attributes;

namespace Management.Infrastructure.Database.Entities.Classroom;

public class ClassroomUpdateStudentsEntity
{
    [BsonRequired]
    public required List<string> StudentsId { get; set; }
}