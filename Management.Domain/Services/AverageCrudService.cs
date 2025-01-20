using Management.Domain.Domains.DTO.Average;
using Management.Domain.Domains.Exceptions;
using Management.Domain.Gateway.Average;
using Management.Domain.Gateway.Student;
using Management.Domain.UseCases.Average;

namespace Management.Domain.Services;

public class AverageCrudService : IAverageCrudUseCase
{
    private readonly IAverageRepositoryGateway _averageRepositoryGateway;
    private readonly IStudentReposityGateway _studentReposityGateway;

    public AverageCrudService(IAverageRepositoryGateway averageRepositoryGateway, IStudentReposityGateway studentReposityGateway)
    {
        _averageRepositoryGateway = averageRepositoryGateway;
        _studentReposityGateway = studentReposityGateway;
    }

    public async Task<AverageDTO> Create(AverageDTO average)
    {
        var existingStudent = await _studentReposityGateway.GetById(average.StudentId);

        if (existingStudent == null)
        {
            throw new AverageException(404, $"It is necessary for the student to exist in order to register a average.");
        }
        
        return await _averageRepositoryGateway.Create(average);
    }

    public async Task<AverageDTO> Update(ScoresDTO score, string averageId)
    {
        var updateAverage = await _averageRepositoryGateway.Update(score, averageId);

        if (updateAverage == null)
        {
            throw new AverageException(404, $"Average with ID {averageId} not found.");
        }

        return updateAverage;
    }

    public async Task<AverageDTO> Delete(string averageId)
    {
        var existingAverage = await _averageRepositoryGateway.Delete(averageId);

        if (existingAverage == null)
        {
            throw new AverageException(404, $"Average with ID {averageId} not found.");
        }

        return existingAverage;
    }

    public async Task<List<AverageDTO>> GetAll()
    {
        return await _averageRepositoryGateway.GetAll();
    }

    public async Task<AverageDTO> GetById(string averageId)
    {
        var existingAverage = await _averageRepositoryGateway.GetById(averageId);

        if (existingAverage == null)
        {
            throw new AverageException(404, $"Average with ID {averageId} not found.");
        }

        return existingAverage;
    }
}