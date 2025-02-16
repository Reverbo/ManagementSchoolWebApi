using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Management.Infrastructure.Database.Entities;

public class AverageEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    [BsonElement("_id")]
    public ObjectId Id { get; set; }
    
    [BsonRequired] 
    public required string DisciplineId { get; set; }
    
    [BsonRequired] 
    public required string StudentId { get; set; }
    
    [BsonRequired] 
    public required string Total { get; set; }
    
    [BsonRequired] 
    public required ScoresEntity Scores { get; set; }
}