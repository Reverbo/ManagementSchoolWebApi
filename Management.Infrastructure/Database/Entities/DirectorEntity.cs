using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Management.Infrasctructure.Database.Entities;

public class DirectorEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    [BsonElement("_id")]
    
    public ObjectId Id { get; set; }
    
    [BsonRequired]
    public required String FullName { get; set; }
    
    [BsonRequired]
    public String Contact { get; set; }
    
}