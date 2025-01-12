using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Management.Infrasctructure.Database.Entities;

public class TeacherEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    [BsonElement("_id")]
    public ObjectId Id { get; set; }

    [BsonRequired]
    public required string FullName { get; set; }

    [BsonRequired]
    public required int Age { get; set; }

    [BsonRequired]
    public required string ClassroomDiscipline { get; set; }

    [BsonRequired]
    public required string Contact { get; set; }

    [BsonRequired]
    public required string ClassTeaching { get; set; }

    [BsonRequired]
    public required decimal Salary { get; set; }
}