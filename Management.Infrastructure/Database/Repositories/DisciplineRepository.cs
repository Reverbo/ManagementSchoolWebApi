using AutoMapper;
using Management.Domain.Domains.DTO.Discipline;
using Management.Domain.Gateway;
using Management.Infrasctructure.Database.Entities;
using Management.Infrastructure.Database.Entities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Management.Infrastructure.Database.Repositories;

public class DisciplineRepository : IDisciplineRepositoryGateway
{
    private readonly IMongoCollection<DisciplineEntity> _disciplines;
    private readonly IMongoCollection<AverageEntity> _averages;
    private readonly IMongoCollection<TeacherEntity> _teachers;
    private readonly IMapper _mapper;

    public DisciplineRepository(IMongoDatabase database, IMapper mapper)
    {
        _disciplines = database.GetCollection<DisciplineEntity>("disciplines");
        _averages = database.GetCollection<AverageEntity>("averages");
        _teachers = database.GetCollection<TeacherEntity>("teachers");
        _mapper = mapper;
    }

    public async Task<DisciplineResponseDTO> Create(DisciplineCreateDTO discipline)
    {
        var disciplineEntity = _mapper.Map<DisciplineEntity>(discipline);
        disciplineEntity.Id = ObjectId.GenerateNewId();
        await _disciplines.InsertOneAsync(disciplineEntity);
        return _mapper.Map<DisciplineResponseDTO>(disciplineEntity);
    }

    public async Task<DisciplineResponseDTO?> Update(DisciplineEditDTO discipline, string disciplineId)
    {
        var disciplineObjectId = new ObjectId(disciplineId);
        var existingDiscipline = await _disciplines.Find(item => item.Id == disciplineObjectId).FirstOrDefaultAsync();

        if (existingDiscipline == null)
        {
            return null;
        }


        existingDiscipline.Name = discipline.Name;
        existingDiscipline.TeacherId = discipline.TeacherId;
        existingDiscipline.BimonthlyId = discipline.BimonthlyId;

        var result = await _disciplines.ReplaceOneAsync(item => item.Id == disciplineObjectId, existingDiscipline);

        if (!result.IsAcknowledged)
        {
            return null;
        }

        var updateDiscipline = await _disciplines.Find(item => item.Id == disciplineObjectId).FirstOrDefaultAsync();
        return _mapper.Map<DisciplineResponseDTO>(updateDiscipline);
    }

    public async Task<DisciplineResponseDTO?> AddAverages(DisciplineUpdateDTO discipline, string disciplineId)
    {
        var disciplineObjectId = new ObjectId(disciplineId);
        var existingDiscipline = await _disciplines.Find(item => item.Id == disciplineObjectId).FirstOrDefaultAsync();

        if (existingDiscipline == null)
        {
            return null;
        }

        foreach (var averageId in discipline.AveragesId)
        {
            existingDiscipline.AveragesId.Add(averageId);
        }

        var averageEntitiesList = existingDiscipline.AveragesId.Select(itemId =>
        {
            var avaregeObjectId = new ObjectId(itemId);
            var averageEntity = _averages.Find(itemAverage => itemAverage.Id == avaregeObjectId).FirstOrDefault();
            return averageEntity;
        }).ToList();

        var disciplineResponse = _mapper.Map<DisciplineResponseEntity>(existingDiscipline);
        disciplineResponse.Averages = averageEntitiesList;

        var result = await _disciplines.ReplaceOneAsync(item => item.Id == disciplineObjectId, existingDiscipline);

        if (!result.IsAcknowledged)
        {
            return null;
        }

        return _mapper.Map<DisciplineResponseDTO>(disciplineResponse);
    }

    public async Task<DisciplineResponseDTO?> RemoveAverages(DisciplineUpdateDTO discipline, string disciplineId)
    {
        var disciplineObjectId = new ObjectId(disciplineId);
        var existingDiscipline = await _disciplines.Find(item => item.Id == disciplineObjectId).FirstOrDefaultAsync();

        if (existingDiscipline == null)
        {
            return null;
        }

        foreach (var averageId in discipline.AveragesId)
        {
            existingDiscipline.AveragesId.Remove(averageId);
        }

        var averageEntitiesList = existingDiscipline.AveragesId.Select(itemId =>
        {
            var avaregeObjectId = new ObjectId(itemId);
            var averageEntity = _averages.Find(itemAverage => itemAverage.Id == avaregeObjectId).FirstOrDefault();
            return averageEntity;
        }).ToList();

        var disciplineResponse = _mapper.Map<DisciplineResponseEntity>(existingDiscipline);
        disciplineResponse.Averages = averageEntitiesList;

        var result = await _disciplines.ReplaceOneAsync(item => item.Id == disciplineObjectId, existingDiscipline);

        if (!result.IsAcknowledged)
        {
            return null;
        }

        return _mapper.Map<DisciplineResponseDTO>(disciplineResponse);
    }

    public async Task<DisciplineDTO?> Delete(string disciplineId)
    {
        var disciplineObjectId = new ObjectId(disciplineId);
        var existingDiscipline = await _disciplines.Find(item => item.Id == disciplineObjectId).FirstOrDefaultAsync();

        if (existingDiscipline == null)
        {
            return null;
        }

        await _disciplines.DeleteOneAsync(item => item.Id == disciplineObjectId);
        return _mapper.Map<DisciplineDTO>(existingDiscipline);
    }

    public async Task<DisciplineDTO?> GetById(string disciplineId)
    {
        var disciplineObjectId = new ObjectId(disciplineId);
        var existingDiscipline = await _disciplines.Find(item => item.Id == disciplineObjectId).FirstOrDefaultAsync();

        if (existingDiscipline == null)
        {
            return null;
        }

        return _mapper.Map<DisciplineDTO>(existingDiscipline);
    }
}