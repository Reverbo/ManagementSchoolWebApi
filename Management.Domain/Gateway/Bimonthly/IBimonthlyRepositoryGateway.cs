using Management.Domain.Domains.DTO.Bimonthly;

namespace Management.Domain.Gateway.Bimonthly;

public interface IBimonthlyRepositoryGateway
{
    Task<BimonthlyResponseDTO> Create(BimonthlyCreateDTO bimonthlyDto);
    Task<BimonthlyResponseDTO?> Update(BimonthlyDatesDTO bimonthly, string bimonthlyId);
    Task<BimonthlyResponseDTO?> AddDisciplines(BimonthlyUpdateDisciplinesDTO bimonthly, string bimonthlyId);
    Task<BimonthlyResponseDTO?> RemoveDisciplines(BimonthlyUpdateDisciplinesDTO bimonthly, string bimonthlyId);
    Task<BimonthlyDTO?> Delete(string bimonthlyId);
    Task<BimonthlyResponseDTO?> GetById(string bimonthlyId);
    Task<List<BimonthlyResponseDTO>?> GetByDate(BimonthlyDatesDTO bimonthly);
}