using AutoMapper;
using Management.Domain.Domains.DTO.Bimonthly;
using Management.Domain.Domains.DTO.Discipline;
using Management.Domain.Gateway;
using Management.Domain.Gateway.Bimonthly;
using Management.Infrastructure.Database.Entities;
using Management.Infrastructure.Database.Entities.Bimonthly;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Management.Infrastructure.Database.Repositories;

public class BimonthlyRepository : IBimonthlyRepositoryGateway
{
    private readonly IMongoCollection<BimonthlyEntity> _bimonthly;
    private readonly IDisciplineRepositoryGateway _discipline;
    private readonly IMapper _mapper;
    public BimonthlyRepository(IMongoDatabase database, IMapper mapper, IDisciplineRepositoryGateway discipline)
    {
        _bimonthly = database.GetCollection<BimonthlyEntity>("bimonthlys");
        _discipline = discipline;
        _mapper = mapper;
    }

    public async Task<BimonthlyResponseDTO> Create(BimonthlyCreateDTO bimonthly)
    {
        var bimonthlyEntity = _mapper.Map<BimonthlyEntity>(bimonthly);
        bimonthlyEntity.Id = ObjectId.GenerateNewId();
        bimonthlyEntity.DisciplinesId = [];
        await _bimonthly.InsertOneAsync(bimonthlyEntity);
        var bimonthlyResponse = _mapper.Map<BimonthlyResponseEntity>(bimonthlyEntity);

        return _mapper.Map<BimonthlyResponseDTO>(bimonthlyResponse);
    }

    public async Task<BimonthlyResponseDTO?> Update(BimonthlyDatesDTO bimonthly, string bimonthlyId)
    {
        var objectId = new ObjectId(bimonthlyId);
        var bimonthlyEntity = await _bimonthly.Find(item => item.Id == objectId).FirstOrDefaultAsync();

        if (bimonthlyEntity == null)
        {
            return null;
        }
        
        bimonthlyEntity.UpdateByBimonthlyDto(bimonthly);
       

        var result = await _bimonthly.ReplaceOneAsync(item => item.Id == objectId, bimonthlyEntity);

        if (!result.IsAcknowledged)
        {
            return null;
        }

        var updatedBimonthly = await _bimonthly.Find(item => item.Id == objectId).FirstOrDefaultAsync();

        var disciplineList = await GetDisciplineList(updatedBimonthly.DisciplinesId);

        var classroomResponse = _mapper.Map<BimonthlyResponseEntity>(bimonthlyEntity);
        classroomResponse.Disciplines = disciplineList;

        return _mapper.Map<BimonthlyResponseDTO>(classroomResponse);
    }

    public async Task<BimonthlyResponseDTO?> AddDisciplines(BimonthlyUpdateDisciplinesDTO bimonthlyDto, 
        string bimonthlyId)
    {
        var objectId = new ObjectId(bimonthlyId);
        var bimonthlyEntity = await _bimonthly.Find(item => item.Id == objectId).FirstOrDefaultAsync();

        if (bimonthlyEntity == null)
        {
            return null;
        }

        foreach (var disciplineId in bimonthlyDto.DisciplinesId)
        {
            bimonthlyEntity.DisciplinesId.Add(disciplineId);
        }

        var disciplineList = await GetDisciplineList(bimonthlyEntity.DisciplinesId);

        var bimonthlyResponse = _mapper.Map<BimonthlyResponseEntity>(bimonthlyEntity);
        bimonthlyResponse.Disciplines = disciplineList;

        var result = await _bimonthly.ReplaceOneAsync(item => item.Id == objectId, bimonthlyEntity);

        if (!result.IsAcknowledged)
        {
            return null;
        }

        return _mapper.Map<BimonthlyResponseDTO>(bimonthlyResponse);
    }

    public async Task<BimonthlyResponseDTO?> RemoveDisciplines(BimonthlyUpdateDisciplinesDTO bimonthlyDto, 
        string bimonthlyId)
    {
        var objectId = new ObjectId(bimonthlyId);
        var bimonthlyEntity = await _bimonthly.Find(item => item.Id == objectId).FirstOrDefaultAsync();

        if (bimonthlyEntity == null)
        {
            return null;
        }

        foreach (var disciplineId in bimonthlyDto.DisciplinesId)
        {
            bimonthlyEntity.DisciplinesId.Remove(disciplineId);
        }

        var disciplinesList = await GetDisciplineList(bimonthlyEntity.DisciplinesId);

        var bimonthlyResponse = _mapper.Map<BimonthlyResponseEntity>(bimonthlyEntity);
        bimonthlyResponse.Disciplines = disciplinesList;

        var result = await _bimonthly.ReplaceOneAsync(item => item.Id == objectId, bimonthlyEntity);

        if (!result.IsAcknowledged)
        {
            return null;
        }

        return _mapper.Map<BimonthlyResponseDTO>(bimonthlyResponse);
    }

    public async Task<BimonthlyDTO?> Delete(string bimonthlyId)
    {
        var bimonthlyObjectId = new ObjectId(bimonthlyId);
        var bimonthlyEntity =
            await _bimonthly.Find(bimonthly => bimonthly.Id == bimonthlyObjectId).FirstOrDefaultAsync();

        if (bimonthlyEntity == null)
        {
            return null;
        }

        await _bimonthly.DeleteOneAsync(item => item.Id == bimonthlyObjectId);

        return _mapper.Map<BimonthlyDTO>(bimonthlyEntity);
    }


    public async Task<BimonthlyResponseDTO?> GetById(string bimonthlyId)
    {
        var bimonthlyObjectId = new ObjectId(bimonthlyId);
        var bimonthlyEntity = await _bimonthly.Find(item => item.Id == bimonthlyObjectId).FirstOrDefaultAsync();

        if (bimonthlyEntity == null)
        {
            return null;
        }

        var disciplinesList = await GetDisciplineList(bimonthlyEntity.DisciplinesId);

        var bimonthlyResponse = _mapper.Map<BimonthlyResponseEntity>(bimonthlyEntity);
        bimonthlyResponse.Disciplines = disciplinesList;

        return _mapper.Map<BimonthlyResponseDTO>(bimonthlyResponse);
    }

    public async Task<List<BimonthlyResponseDTO>?> GetByDate(BimonthlyDatesDTO bimonthly)
    {
        var bimonthlyEntity = await _bimonthly.Find(_ => true).ToListAsync();

        if (bimonthlyEntity == null)
        {
            return null;
        }

        var bimonthlyDateFilter = bimonthlyEntity.FindAll(itemBimonthly =>
            DateTime.Parse(itemBimonthly.StartDate) > DateTime.Parse(bimonthly.StartDate) &&
            DateTime.Parse(itemBimonthly.EndDate) < DateTime.Parse(bimonthly.EndDate)
        ).ToList();

        var bimonthlyResponseDtoList = new List<BimonthlyResponseEntity>();

        foreach (var bimonthlyItem in bimonthlyDateFilter)
        {
            var disciplineList = await GetDisciplineList(bimonthlyItem.DisciplinesId);
            var bimonthlyMap = _mapper.Map<BimonthlyResponseEntity>(bimonthlyItem);
            bimonthlyMap.Disciplines = disciplineList;
            bimonthlyResponseDtoList.Add(bimonthlyMap);
        }

        return _mapper.Map<List<BimonthlyResponseDTO>>(bimonthlyResponseDtoList).ToList();
    }

    private async Task<List<DisciplineResponseEntity>> GetDisciplineList(List<string> disciplineIds)
    {
        var disciplines = new List<DisciplineResponseDTO>();
        foreach (var disciplineId in disciplineIds)
        {
            var disciplineGetById = await _discipline.GetById(disciplineId);
            if (disciplineGetById != null)
            {
                disciplines.Add(disciplineGetById);
            }
        }

        return _mapper.Map<List<DisciplineResponseEntity>>(disciplines);
    }
}