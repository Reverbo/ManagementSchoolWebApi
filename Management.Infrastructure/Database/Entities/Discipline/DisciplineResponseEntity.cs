using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Management.Infrastructure.Database.Entities;

public class DisciplineResponseEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    [BsonElement("_id")]
    
    public ObjectId Id { get; set; }
    
    [BsonRequired]
    public required string Name { get; set; }
    
    [BsonRequired]
    public required string BimonthlyId { get; set; }
    
    [BsonRequired]
    public required string TeacherId {get; set;}
    
    [BsonRequired]
    public required List<AverageEntity> Averages { get; set; }
    
}