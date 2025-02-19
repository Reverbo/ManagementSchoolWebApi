using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Management.Infrastructure.Database.Entities.Bimonthly;

public class BimonthlyResponseEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    [BsonElement("_id")]
    public ObjectId Id { get; set; }
    
    [BsonRequired] 
    public required string StartDate { get; set; }

    [BsonRequired] 
    public required string EndDate { get; set; }
    
    [BsonRequired] 
    public required string ClassroomId { get; set; }
    
    [BsonRequired] 
    public required List<DisciplineResponseEntity> Disciplines { get; set; }
}