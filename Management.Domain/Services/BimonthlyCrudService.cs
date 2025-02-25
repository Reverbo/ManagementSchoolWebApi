using Management.Domain.Domains.DTO.Bimonthly;
using Management.Domain.Domains.Exceptions;
using Management.Domain.Domains.Exceptions.Bimonthly;
using Management.Domain.Gateway;
using Management.Domain.Gateway.Bimonthly;
using Management.Domain.Gateway.Classroom;
using Management.Domain.UseCases.Bimonthly;

namespace Management.Domain.Services;

public class BimonthlyCrudService : IBimonthlyCrudUseCase
{
    private readonly IBimonthlyRepositoryGateway _bimonthlyRepositoryGateway;
    private readonly IClassroomRepositoryGateway _classroomRepositoryGateway;
    private readonly IDisciplineRepositoryGateway _disciplineRepositoryGateway;

    public BimonthlyCrudService(IBimonthlyRepositoryGateway bimonthlyCrudUseCase,
        IClassroomRepositoryGateway classroomRepositoryGateway,
        IDisciplineRepositoryGateway disciplineRepositoryGateway)
    {
        _bimonthlyRepositoryGateway = bimonthlyCrudUseCase;
        _classroomRepositoryGateway = classroomRepositoryGateway;
        _disciplineRepositoryGateway = disciplineRepositoryGateway;
    }

    public async Task<BimonthlyResponseDTO> Create(BimonthlyCreateDTO bimonthlyDto)
    {
        await ValidateBimonthly(bimonthlyDto);
        return await _bimonthlyRepositoryGateway.Create(bimonthlyDto);
    }

    public async Task<BimonthlyResponseDTO> Update(BimonthlyDatesDTO bimonthlyDto, string bimonthlyId)
    {
        ValidatePeriod(bimonthlyDto.StartDate, bimonthlyDto.EndDate);

        var bimonthly = await _bimonthlyRepositoryGateway.Update(bimonthlyDto, bimonthlyId);

        if (bimonthly == null)
        {
            throw new BimonthlyNotFoundException(404, $"Bimonthly with ID {bimonthlyId} not found.");
        }

        return bimonthly;
    }

    public async Task<BimonthlyResponseDTO?> AddDisciplines(BimonthlyUpdateDisciplinesDTO bimonthlyDto,
        string bimonthlyId)
    {
        await ValidateDisciplinesExistence(bimonthlyDto.DisciplinesId);

        var disciplinesGetIds = await GetBimonthlyDisciplinesIds(bimonthlyId);

        foreach (var disciplineId in bimonthlyDto.DisciplinesId)
        {
            if (disciplinesGetIds.Contains(disciplineId))
            {
                throw new DisciplineAlreadyException(400,
                    $"Unable to add disciplines. Discipline ID {disciplineId} has already been added.");
            }
        }

        var bimonthly = await _bimonthlyRepositoryGateway.AddDisciplines(bimonthlyDto, bimonthlyId);

        return bimonthly;
    }

    public async Task<BimonthlyResponseDTO?> RemoveDisciplines(BimonthlyUpdateDisciplinesDTO bimonthlyDto,
        string bimonthlyId)
    {
        await ValidateDisciplinesExistence(bimonthlyDto.DisciplinesId);

        var disciplinesGetIds = await GetBimonthlyDisciplinesIds(bimonthlyId);

        if (disciplinesGetIds.Count == 0)
        {
            throw new BimonthlyException(404, $"Disciplines with ID {bimonthlyId} has not students.");
        }

        foreach (var disciplineId in bimonthlyDto.DisciplinesId)
        {
            if (!disciplinesGetIds.Contains(disciplineId))
            {
                throw new DisciplineNotFoundForStudentException(400,
                    $"Unable to remove disciplines. The discipline {disciplineId} could not be removed because he does not exist in this bimonthly.");
            }
        }

        var bimonthly = await _bimonthlyRepositoryGateway.RemoveDisciplines(bimonthlyDto, bimonthlyId);

        return bimonthly;
    }

    public async Task Delete(string bimonthlyId)
    {
        var existingBimonthly = await _bimonthlyRepositoryGateway.Delete(bimonthlyId) != null;

        if (!existingBimonthly)
        {
            throw new BimonthlyNotFoundException(404, $"Bimonthly with ID {bimonthlyId} not found.");
        }
    }

    public async Task<BimonthlyResponseDTO> GetById(string bimonthlyId)
    {
        var bimonthly = await _bimonthlyRepositoryGateway.GetById(bimonthlyId);

        if (bimonthly == null)
        {
            throw new BimonthlyNotFoundException(404, $"Bimonthly with ID {bimonthlyId} not found.");
        }

        return bimonthly;
    }

    public async Task<List<BimonthlyResponseDTO>> GetByDate(BimonthlyDatesDTO bimonthlyDates)
    {
        var dateIsInvalid = !DateTime.TryParse(bimonthlyDates.StartDate, out _) ||
                            !DateTime.TryParse(bimonthlyDates.EndDate, out _);

        if (dateIsInvalid)
        {
            throw new BimonthlyInvalidDateException(400,
                "Invalid date. The start date and/or the end date are not valid.");
        }
        
        var bimonthlyList = await _bimonthlyRepositoryGateway.GetByDate(bimonthlyDates);

        if (bimonthlyList == null)
        {
            throw new BimonthlyInvalidDateException(404,
                $"Bimonthly with this period not found. StartDate: {bimonthlyDates.StartDate} | EndStar: {bimonthlyDates.EndDate}");
        }

        return bimonthlyList;
    }

    private async Task ValidateDisciplinesExistence(List<string> studentIdList)
    {
        foreach (var itemClassroomId in studentIdList)
        {
            var existingDiscipline = await _disciplineRepositoryGateway.GetById(itemClassroomId);

            if (existingDiscipline == null)
            {
                throw new DisciplineNotFoundException(404,
                    $"The following discipline ID do not exist: {itemClassroomId}");
            }
        }
    }

    private async Task ValidateBimonthly(BimonthlyCreateDTO bimonthlyDto)
    {
        var existingClassroom = await _classroomRepositoryGateway.GetById(bimonthlyDto.ClassroomId) != null;

        if (!existingClassroom)
        {
            throw new ClassroomNotFoundException(404, $"Classroom with ID  {bimonthlyDto.ClassroomId} not found.");
        }
        
        ValidatePeriod(bimonthlyDto.StartDate, bimonthlyDto.EndDate);
    }

    private void ValidatePeriod(string starDate, string endDate)
    {
        var dateIsInvalid = !DateTime.TryParse(starDate, out DateTime startDateBimonthly) ||
                            !DateTime.TryParse(endDate, out DateTime endDateBimonthly) ||
                            startDateBimonthly < DateTime.Now ||
                            endDateBimonthly < startDateBimonthly;

        if (dateIsInvalid)
        {
            throw new BimonthlyInvalidDateException(400,
                "Invalid date. The start date must be after the current date and the end date cannot be before the start date.");
        }
    }

    private async Task<List<string>> GetBimonthlyDisciplinesIds(string bimonthlyId)
    {
        var bimonthlyGetById = await _bimonthlyRepositoryGateway.GetById(bimonthlyId);

        if (bimonthlyGetById == null)
        {
            throw new DisciplineNotFoundException(404, $"Classroom with ID {bimonthlyId} not found.");
        }

        var bimonthlyGetIds =
            bimonthlyGetById.Disciplines.Select(discipline => discipline.Id.ToString()).ToList() ?? [];

        return bimonthlyGetIds;
    }
}