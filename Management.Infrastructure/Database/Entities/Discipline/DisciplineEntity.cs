using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Management.Infrastructure.Database.Entities;

public class DisciplineEntity
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
    public ObjectId TeacherId {get; set;}

    [BsonRequired] 
    public required List<string> AveragesId { get; set; } = [];
}