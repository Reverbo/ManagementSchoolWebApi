using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Management.Infrastructure.Database.Entities;

public class StudentEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    [BsonElement("_id")]
    public ObjectId Id { get; set; }
    
    [BsonRequired] 
    public required string FullName { get; set; }
    
    [BsonRequired]
    public required string SocialName { get; set; }
    
    [BsonRequired]
    public required string DateBirth { get; set; }
    
    [BsonRequired]
    public required string ClassroomId { get; set; }
    
    [BsonRequired]
    public required string Cpf {get; set;}
    
    [BsonRequired]
    public required string Rg { get; set; }
    
    [BsonRequired]
    public required string Email { get; set; } 
    
    [BsonRequired]
    public required string FatherName { get; set; }
    
    [BsonRequired]
    public required string MotherName { get; set; }
   
}