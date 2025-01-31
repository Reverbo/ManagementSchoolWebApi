using Management.Domain.Domains.DTO.Students;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Management.Infrastructure.Database.Entities;

public class ClassroomUpdateEntity
{
    [BsonRequired]
    public required List<string> StudentsId { get; set; }
}