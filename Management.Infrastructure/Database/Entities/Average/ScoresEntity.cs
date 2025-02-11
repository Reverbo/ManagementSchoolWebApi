using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Management.Infrastructure.Database.Entities;

public class ScoresEntity
{
    [BsonRequired] 
    public required double FirstScore { get; set; }
    
    [BsonRequired]
    public required double SecondScore { get; set; }
}