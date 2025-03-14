using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Management.Infrastructure.Database.Entities;

public class ClassroomResponseEntity
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
    public required List<StudentEntity> Students { get; set; }
    
}