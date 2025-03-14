using AutoMapper;
using Management.Domain.Domains.DTO.Average;
using Management.Domain.Domains.DTO.Discipline;
using Management.Domain.Gateway;
using Management.Domain.Gateway.Average;
using Management.Infrastructure.Database.Entities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Management.Infrastructure.Database.Repositories;

public class DisciplineRepository : IDisciplineRepositoryGateway
{
    private readonly IMongoCollection<DisciplineEntity> _disciplines;
    private readonly IAverageRepositoryGateway _averages;
    private readonly IMapper _mapper;

    public DisciplineRepository(IMongoDatabase database, IMapper mapper, IAverageRepositoryGateway averages)
    {
        _disciplines = database.GetCollection<DisciplineEntity>("disciplines");
        _averages = averages;
        _mapper = mapper;
    }

    public async Task<DisciplineResponseDTO> Create(DisciplineCreateDTO discipline)
    {
        var disciplineEntity = _mapper.Map<DisciplineEntity>(discipline);
        disciplineEntity.Id = ObjectId.GenerateNewId();
        await _disciplines.InsertOneAsync(disciplineEntity);
        return _mapper.Map<DisciplineResponseDTO>(disciplineEntity);
    }

    public async Task<DisciplineResponseDTO?> Update(DisciplineUpdateDTO discipline, string disciplineId)
    {
        var disciplineObjectId = new ObjectId(disciplineId);
        var disciplineEntity = await _disciplines.Find(item => item.Id == disciplineObjectId).FirstOrDefaultAsync();

        if (disciplineEntity == null)
        {
            return null;
        }

       disciplineEntity.UpdateByDisciplineDto(discipline);

        var result = await _disciplines.ReplaceOneAsync(item => item.Id == disciplineObjectId, disciplineEntity);

        if (!result.IsAcknowledged)
        {
            return null;
        }

        var updateDiscipline = await _disciplines.Find(item => item.Id == disciplineObjectId).FirstOrDefaultAsync();
        return _mapper.Map<DisciplineResponseDTO>(updateDiscipline);
    }

    public async Task<DisciplineResponseDTO?> AddAverages(DisciplineUpdateAveragesDTO discipline, string disciplineId)
    {
        var disciplineObjectId = new ObjectId(disciplineId);
        var disciplineEntity = await _disciplines.Find(item => item.Id == disciplineObjectId).FirstOrDefaultAsync();

        if (disciplineEntity == null)
        {
            return null;
        }

        foreach (var averageId in discipline.AveragesId)
        {
            disciplineEntity.AveragesId.Add(averageId);
        }

        var averageEntitiesList = await GetAverageList(disciplineEntity.AveragesId);

        var disciplineResponse = _mapper.Map<DisciplineResponseEntity>(disciplineEntity);
        disciplineResponse.Averages = averageEntitiesList;

        var result = await _disciplines.ReplaceOneAsync(item => item.Id == disciplineObjectId, disciplineEntity);

        if (!result.IsAcknowledged)
        {
            return null;
        }

        return _mapper.Map<DisciplineResponseDTO>(disciplineResponse);
    }

    public async Task<DisciplineResponseDTO?> RemoveAverages(DisciplineUpdateAveragesDTO discipline,
        string disciplineId)
    {
        var disciplineObjectId = new ObjectId(disciplineId);
        var disciplineEntity = await _disciplines.Find(item => item.Id == disciplineObjectId).FirstOrDefaultAsync();

        if (disciplineEntity == null)
        {
            return null;
        }

        foreach (var averageId in discipline.AveragesId)
        {
            disciplineEntity.AveragesId.Remove(averageId);
        }

        var averageEntitiesList = await GetAverageList(disciplineEntity.AveragesId);

        var disciplineResponse = _mapper.Map<DisciplineResponseEntity>(disciplineEntity);
        disciplineResponse.Averages = averageEntitiesList;

        var result = await _disciplines.ReplaceOneAsync(item => item.Id == disciplineObjectId, disciplineEntity);

        if (!result.IsAcknowledged)
        {
            return null;
        }

        return _mapper.Map<DisciplineResponseDTO>(disciplineResponse);
    }

    public async Task<DisciplineDTO?> Delete(string disciplineId)
    {
        var disciplineObjectId = new ObjectId(disciplineId);
        var disciplineEntity = await _disciplines.Find(item => item.Id == disciplineObjectId).FirstOrDefaultAsync();

        if (disciplineEntity == null)
        {
            return null;
        }

        await _disciplines.DeleteOneAsync(item => item.Id == disciplineObjectId);
        return _mapper.Map<DisciplineDTO>(disciplineEntity);
    }

    public async Task<DisciplineResponseDTO?> GetById(string disciplineId)
    {
        var disciplineObjectId = new ObjectId(disciplineId);
        var disciplineEntity = await _disciplines.Find(item => item.Id == disciplineObjectId).FirstOrDefaultAsync();

        if (disciplineEntity == null)
        {
            return null;
        }
        var averageList = await GetAverageList(disciplineEntity.AveragesId);

        var disciplineResponse = _mapper.Map<DisciplineResponseEntity>(disciplineEntity);
        disciplineResponse.Averages = averageList;

        return _mapper.Map<DisciplineResponseDTO>(disciplineResponse);
    }
    
    private async Task<List<AverageEntity>> GetAverageList(List<string> averageIds)
    {
        var averages = new List<AverageDTO>();
        foreach (var averageId in averageIds)
        {
            var averageGetById = await _averages.GetById(averageId);
            if (averageGetById != null)
            {
                averages.Add(averageGetById);
            }
        }

        return _mapper.Map<List<AverageEntity>>(averages);
    }
    
}