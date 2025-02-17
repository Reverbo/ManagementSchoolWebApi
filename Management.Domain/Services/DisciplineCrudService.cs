using System.ComponentModel;
using Management.Domain.Domains.DTO.Discipline;
using Management.Domain.Domains.Exceptions;
using Management.Domain.Gateway;
using Management.Domain.Gateway.Average;
using Management.Domain.Gateway.Bimonthly;
using Management.Domain.Gateway.Teacher;
using Management.Domain.UseCases.Discipline;


namespace Management.Domain.Services;

public class DisciplineCrudService : IDisciplineCrudUseCase
{
    private readonly IDisciplineRepositoryGateway _disciplineRepositoryGateway;
    private readonly IAverageRepositoryGateway _averageRepositoryGateway;
    private readonly ITeacherRepositoryGateway _teacherRepositoryGateway;
    private readonly IBimonthlyRepositoryGateway _bimonthlyRepositoryGateway;

    public DisciplineCrudService
    (IDisciplineRepositoryGateway disciplineRepositoryGateway,
        IAverageRepositoryGateway averageRepositoryGateway,
        ITeacherRepositoryGateway teacherRepositoryGateway,
        IBimonthlyRepositoryGateway bimonthlyRepositoryGateway)
    {
        _disciplineRepositoryGateway = disciplineRepositoryGateway;
        _averageRepositoryGateway = averageRepositoryGateway;
        _teacherRepositoryGateway = teacherRepositoryGateway;
        _bimonthlyRepositoryGateway = bimonthlyRepositoryGateway;
    }

    public async Task<DisciplineResponseDTO> Create(DisciplineCreateDTO disciplineDto)
    {
        var existingBimonthly = await _bimonthlyRepositoryGateway.GetById(disciplineDto.BimonthlyId) != null;
        var existingTeacherId = await _teacherRepositoryGateway.GetById(disciplineDto.TeacherId) != null;
        if (!existingBimonthly)
        {
            throw new DisciplineException(404, $"Bimonthly with ID {disciplineDto.BimonthlyId} not found.");
        }
        if (!existingTeacherId)
        {
            throw new DisciplineException(404, $"Teacher with ID {disciplineDto.TeacherId} not found.");
        }

        return await _disciplineRepositoryGateway.Create(disciplineDto);
    }

    public async Task<DisciplineResponseDTO> Update(DisciplineUpdateDTO disciplineDto, string disciplineId)
    {
        var existingBimonthly = await _bimonthlyRepositoryGateway.GetById(disciplineDto.BimonthlyId) != null;
        if (!existingBimonthly)
        {
            throw new DisciplineException(404, $"Bimonthly with ID {disciplineDto.BimonthlyId} not found.");
        }
        
        var existingTeacherId = await _teacherRepositoryGateway.GetById(disciplineDto.TeacherId) != null;
        
        if (!existingTeacherId)
        {
            throw new DisciplineException(404, $"Teacher with ID {disciplineDto.TeacherId} not found.");
        }

        var existingDiscipline = await _disciplineRepositoryGateway.Update(disciplineDto, disciplineId);

        if (existingDiscipline == null)
        {
            throw new DisciplineException(404, $"Discipline with ID {disciplineId} not found.");
        }

        return existingDiscipline;
    }

    public async Task<DisciplineResponseDTO> AddAverages(DisciplineUpdateAveragesDTO disciplineDto, string disciplineId)
    {
        var existingDiscipline = await _disciplineRepositoryGateway.GetById(disciplineId) != null;
        
        if (!existingDiscipline)
        {
            throw new DisciplineException(404, $"Discipline with ID {disciplineId} not found.");
        }

        foreach (var itemAverageId in disciplineDto.AveragesId)
        {
            var existingAverage = await _averageRepositoryGateway.GetById(itemAverageId) != null;

            if (!existingAverage)
            {
                throw new DisciplineException(404,
                    $"The average Id: {itemAverageId} does not exist.");
            }

            var existingAverageIds = await GetAverageIds(disciplineId);
            if (existingAverageIds.Contains(itemAverageId))
            {
                throw new DisciplineException(404,
                    $"The average Id: {itemAverageId} already exist.");
            }
        }

        var newAverage = await _disciplineRepositoryGateway.AddAverages(disciplineDto, disciplineId);
        return newAverage!;
    }

    public async Task<DisciplineResponseDTO> RemoveAverages(DisciplineUpdateAveragesDTO disciplineDto, string disciplineId)
    {
        var existingDiscipline = await _disciplineRepositoryGateway.GetById(disciplineId) != null;
        if (!existingDiscipline)
        {
            throw new DisciplineException(404, $"Discipline with ID {disciplineId} not found.");
        }

        foreach (var itemAverageId in disciplineDto.AveragesId)
        {
            var existingAverage = await _averageRepositoryGateway.GetById(itemAverageId) != null;

            if (!existingAverage)
            {
                throw new DisciplineException(404,
                    $"The following average IDs do not exist: {itemAverageId}");
            }

            var getAveragesRemoveId = await GetAverageIds(disciplineId);
            if (!getAveragesRemoveId.Contains(itemAverageId))
            {
                throw new DisciplineException(404,
                    $"The average ID: {itemAverageId} does not exist in discipline: {disciplineId}.");
            }
        }

        var removeAverage = await _disciplineRepositoryGateway.RemoveAverages(disciplineDto, disciplineId);
        return removeAverage!;
    }

    public async Task Delete(string disciplineId)
    {
        var existingDiscipline = await _disciplineRepositoryGateway.Delete(disciplineId) !=null;

        if (!existingDiscipline)
        {
            throw new DisciplineException(404, $"Discipline with ID {disciplineId} not found.");
        }

    }

    public async Task<DisciplineResponseDTO> GetById(string disciplineId)
    {
        var existingDiscipline = await _disciplineRepositoryGateway.GetById(disciplineId);

        if (existingDiscipline == null)
        {
            throw new DisciplineException(404, $"Discipline with ID {disciplineId} not found.");
        }

        return existingDiscipline;
    }

    private async Task<List<string>> GetAverageIds(string disciplineId)
    {
        var itemDiscipline = await _disciplineRepositoryGateway.GetByAverage(disciplineId);
        var existingAverageIds = itemDiscipline?.AveragesId.Select(itemId => itemId.ToString()).ToList();
        return existingAverageIds ?? [];
    }
}