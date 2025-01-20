using System.Globalization;
using AutoMapper;
using Management.Domain.Domains.DTO.Average;
using Management.Domain.Gateway.Average;
using Management.Infrastructure.Database.Entities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Management.Infrastructure.Database.Repositories;

public class AverageRepository : IAverageRepositoryGateway
{
    private readonly IMongoCollection<AverageEntity> _averages;
    private readonly IMapper _mapper;

    public AverageRepository(IMongoDatabase database, IMapper mapper)
    {
        _averages = database.GetCollection<AverageEntity>("averages");
        _mapper = mapper;
    }
    
    public async Task<AverageDTO> Create(AverageDTO average)
    {
        var averageEntity = _mapper.Map<AverageEntity>(average);
        averageEntity.Id = ObjectId.GenerateNewId();
        await _averages.InsertOneAsync(averageEntity);
        return _mapper.Map<AverageDTO>(averageEntity);
    }

    public async Task<AverageDTO?> Update(ScoresDTO score, string averageId)
    {
        var averageObjectId = new ObjectId(averageId);
        var existingAverage = await _averages.Find(item => item.Id == averageObjectId).FirstOrDefaultAsync();

        if (existingAverage == null )
        {
            return null;
        }

        existingAverage.Scores.FirstScore = score.FirstScore;
        existingAverage.Scores.SecondScore = score.SecondScore;
        existingAverage.Total = ((existingAverage.Scores.FirstScore + existingAverage.Scores.SecondScore) / 2.0).ToString(CultureInfo.InvariantCulture);
        
        var result = await _averages.ReplaceOneAsync(item => item.Id == averageObjectId, existingAverage);
        
        if (!result.IsAcknowledged)
        {
            return null;
        }
        var updateAverage = await _averages.Find(item => item.Id == averageObjectId).FirstOrDefaultAsync();
        return _mapper.Map<AverageDTO>(updateAverage);
    }

    public async Task<AverageDTO?> Delete(string averageId)
    {
        var averageObjectId = new ObjectId(averageId);
        var existingAverage = await _averages.Find(average => average.Id == averageObjectId).FirstOrDefaultAsync();

        if (existingAverage == null)
        {
            return null;
        }
        
        await _averages.DeleteOneAsync(item => item.Id == averageObjectId);
        
        return _mapper.Map<AverageDTO>(existingAverage);
    }

    public async Task<List<AverageDTO>> GetAll()
    {
        var averageList = await _averages.Find(_ => true).ToListAsync();

        return _mapper.Map<List<AverageDTO>>(averageList).ToList();
    }

    public async Task<AverageDTO?> GetById(string averageId)
    {
        var averageObjectId = new ObjectId(averageId);
        var existingAverage = await _averages.Find(item => item.Id == averageObjectId).FirstOrDefaultAsync();

        if (existingAverage == null)
        {
            return null;
        }

        return _mapper.Map<AverageDTO>(existingAverage);
    }
}