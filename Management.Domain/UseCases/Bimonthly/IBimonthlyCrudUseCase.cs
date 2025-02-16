using Management.Domain.Domains.DTO.Bimonthly;

namespace Management.Domain.UseCases.Bimonthly;

public interface IBimonthlyCrudUseCase
{
    Task<BimonthlyResponseDTO> Create(BimonthlyDTO bimonthlyDto);
    Task<BimonthlyResponseDTO> Update(BimonthlyDatesDTO bimonthly, string bimonthlyId);
    Task<BimonthlyResponseDTO?> AddDisciplines(BimonthlyUpdateDisciplinesDTO bimonthly, string bimonthlyId);
    Task<BimonthlyResponseDTO?> RemoveDisciplines(BimonthlyUpdateDisciplinesDTO bimonthly, string bimonthlyId);
    Task Delete(string bimonthlyId);
    Task<BimonthlyResponseDTO> GetById(string bimonthlyId);
    Task<List<BimonthlyResponseDTO>> GetByDate(BimonthlyDatesDTO bimonthly);
}