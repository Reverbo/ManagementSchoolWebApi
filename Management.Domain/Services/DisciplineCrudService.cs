using System.ComponentModel;
using Management.Domain.Domains.DTO.Discipline;
using Management.Domain.Domains.Exceptions;
using Management.Domain.Gateway;
using Management.Domain.Gateway.Average;
using Management.Domain.Gateway.Teacher;
using Management.Domain.UseCases.Discipline;


namespace Management.Domain.Services;

public class DisciplineCrudService : IDisciplineCrudUseCase
{
    private readonly IDisciplineRepositoryGateway _disciplineRepositoryGateway;
    private readonly IAverageRepositoryGateway _averageRepositoryGateway;
    private readonly ITeacherRepositoryGateway _teacherRepositoryGateway;

    public DisciplineCrudService
    (IDisciplineRepositoryGateway disciplineRepositoryGateway,
        IAverageRepositoryGateway averageRepositoryGateway,
        ITeacherRepositoryGateway teacherRepositoryGateway)
    {
        _disciplineRepositoryGateway = disciplineRepositoryGateway;
        _averageRepositoryGateway = averageRepositoryGateway;
        _teacherRepositoryGateway = teacherRepositoryGateway;
    }

    public async Task<DisciplineResponseDTO> Create(DisciplineCreateDTO discipline)
    {
        var existingTeacherId = await _teacherRepositoryGateway.GetById(discipline.TeacherId);
        if (existingTeacherId == null)
        {
            throw new DisciplineException(404, $"Teacher with ID {discipline.TeacherId} not found.");
        }

        return await _disciplineRepositoryGateway.Create(discipline);
    }

    public async Task<DisciplineResponseDTO> Update(DisciplineUpdateDTO discipline, string disciplineId)
    {
        var existingTeacherId = await _teacherRepositoryGateway.GetById(discipline.TeacherId);
        if (existingTeacherId == null)
        {
            throw new DisciplineException(404, $"Teacher with ID {discipline.TeacherId} not found.");
        }

        var existingDiscipline = await _disciplineRepositoryGateway.Update(discipline, disciplineId);

        if (existingDiscipline == null)
        {
            throw new DisciplineException(404, $"Discipline with ID {disciplineId} not found.");
        }

        return existingDiscipline;
    }

    public async Task<DisciplineResponseDTO> AddAverages(DisciplineUpdateAveragesDTO discipline, string disciplineId)
    {
        var existingDiscipline = await _disciplineRepositoryGateway.GetById(disciplineId);

        if (existingDiscipline == null)
        {
            throw new DisciplineException(404, $"Discipline with ID {disciplineId} not found.");
        }

        foreach (var itemAverageId in discipline.AveragesId)
        {
            var existingAverage = await _averageRepositoryGateway.GetById(itemAverageId);

            if (existingAverage == null)
            {
                throw new DisciplineException(404,
                    $"The average Id: {itemAverageId} does not exist.");
            }

            var existingAverageIds = await GetDisciplinesAveragesIds(disciplineId);
            if (existingAverageIds.Contains(itemAverageId))
            {
                throw new DisciplineException(404,
                    $"The average Id: {itemAverageId} already exist.");
            }
        }

        var newAverage = await _disciplineRepositoryGateway.AddAverages(discipline, disciplineId);
        return newAverage!;
    }

    public async Task<DisciplineResponseDTO> RemoveAverages(DisciplineUpdateAveragesDTO discipline, string disciplineId)
    {
        var existingDiscipline = await _disciplineRepositoryGateway.GetById(disciplineId);
        if (existingDiscipline == null)
        {
            throw new DisciplineException(404, $"Discipline with ID {disciplineId} not found.");
        }

        foreach (var itemAverageId in discipline.AveragesId)
        {
            var existingAverage = await _averageRepositoryGateway.GetById(itemAverageId);

            if (existingAverage == null)
            {
                throw new DisciplineException(404,
                    $"The following average IDs do not exist: {itemAverageId}");
            }

            var getAveragesRemoveId = await GetDisciplinesAveragesIds(disciplineId);
            if (!getAveragesRemoveId.Contains(itemAverageId))
            {
                throw new DisciplineException(404,
                    $"The average ID: {itemAverageId} does not exist in discipline: {disciplineId}.");
            }
        }

        var removeAverage = await _disciplineRepositoryGateway.RemoveAverages(discipline, disciplineId);
        return removeAverage!;
    }

    public async Task<DisciplineDTO> Delete(string disciplineId)
    {
        var existingDiscipline = await _disciplineRepositoryGateway.Delete(disciplineId);

        if (existingDiscipline == null)
        {
            throw new DisciplineException(404, $"Discipline with ID {disciplineId} not found.");
        }

        return existingDiscipline;
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

    private async Task<List<string>> GetDisciplinesAveragesIds(string disciplineId)
    {
        var disciplineGetById = await _disciplineRepositoryGateway.GetById(disciplineId);

        if (disciplineGetById == null)
        {
            throw new BimonthlyException(404, $"Discipline with ID {disciplineId} not found.");
        }

        var disciplineGetIds =
            disciplineGetById.Averages.Select(average => average.Id.ToString()).ToList() ?? [];

        return disciplineGetIds;
    }
}