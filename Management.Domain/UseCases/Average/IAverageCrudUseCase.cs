using Management.Domain.Domains.DTO.Average;

namespace Management.Domain.UseCases.Average;

public interface IAverageCrudUseCase
{
    Task<AverageDTO> Create(AverageCreateDTO average);
    Task<AverageDTO> Update(ScoresDTO score, string averageId);
    Task<AverageDTO> Delete(string averageId);
    Task<List<AverageDTO>> GetAll();
    Task<AverageDTO> GetById(string averageId);
}