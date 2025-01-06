using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Management.Infrasctructure.Database.Entities;

public class Student
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    [BsonElement("Name")] 
    public String FirstName { get; set; }
    public int Age { get; set; }
}