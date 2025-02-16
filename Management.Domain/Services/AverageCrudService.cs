using Management.Domain.Domains.DTO.Average;
using Management.Domain.Domains.Exceptions;
using Management.Domain.Gateway;
using Management.Domain.Gateway.Average;
using Management.Domain.Gateway.Student;
using Management.Domain.UseCases.Average;

namespace Management.Domain.Services;

public class AverageCrudService : IAverageCrudUseCase
{
    private readonly IAverageRepositoryGateway _averageRepositoryGateway;
    private readonly IStudentReposityGateway _studentReposityGateway;
    private readonly IDisciplineRepositoryGateway _disciplineRepositoryGateway;

    public AverageCrudService(IAverageRepositoryGateway averageRepositoryGateway,
        IStudentReposityGateway studentReposityGateway, IDisciplineRepositoryGateway disciplineRepositoryGateway)
    {
        _averageRepositoryGateway = averageRepositoryGateway;
        _studentReposityGateway = studentReposityGateway;
        _disciplineRepositoryGateway = disciplineRepositoryGateway;
    }

    public async Task<AverageDTO> Create(AverageDTO average)
    {
        var existingStudent = await _studentReposityGateway.GetById(average.StudentId) != null;

        if (!existingStudent)
        {
            throw new AverageException(404,
                $"It is necessary for the student to exist in order to register a average.");
        }

        var existingDiscipline = await _disciplineRepositoryGateway.GetById(average.DisciplineId) != null;

        if (!existingDiscipline)
        {
            throw new AverageException(404,
                $"It is necessary for the discipline to exist in order to register a average.");
        }

        return await _averageRepositoryGateway.Create(average);
    }

    public async Task<AverageDTO> Update(ScoresDTO score, string averageId)
    {
        var average = await _averageRepositoryGateway.Update(score, averageId);

        if (average == null)
        {
            throw new AverageException(404, $"Average with ID {averageId} not found.");
        }

        var scoresIsValid = score.FirstScore is >= 0 and <= 10 &&
                            score.SecondScore is >= 0 and <= 10;
        
        if (!scoresIsValid)
        {
            throw new AverageException(404, $"Scores must be between 0 and 10.");
        }

        return average;
    }

    public async Task<AverageDTO> Delete(string averageId)
    {
        var average = await _averageRepositoryGateway.Delete(averageId);

        if (average == null)
        {
            throw new AverageException(404, $"Average with ID {averageId} not found.");
        }

        return average;
    }

    public async Task<List<AverageDTO>> GetAll()
    {
        return await _averageRepositoryGateway.GetAll();
    }

    public async Task<AverageDTO> GetById(string averageId)
    {
        var average = await _averageRepositoryGateway.GetById(averageId);

        if (average == null)
        {
            throw new AverageException(404, $"Average with ID {averageId} not found.");
        }

        return average;
    }
}