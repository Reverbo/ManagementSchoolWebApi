using System.Runtime.InteropServices.JavaScript;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Management.Infrasctructure.Database.Entities;

public class StudentEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    [BsonElement("_id")]
    public ObjectId Id { get; set; }
    
    [BsonRequired] 
    public required string FirstName { get; set; }
    
    [BsonRequired]
    public required int Age { get; set; }
    
}