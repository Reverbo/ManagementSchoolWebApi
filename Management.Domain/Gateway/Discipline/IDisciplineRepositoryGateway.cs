using Management.Domain.Domains.DTO.Discipline;

namespace Management.Domain.Gateway;

public interface IDisciplineRepositoryGateway
{
    Task<DisciplineResponseDTO> Create(DisciplineCreateDTO discipline);
    Task<DisciplineResponseDTO?> Update(DisciplineUpdateDTO discipline, string disciplineId);
    Task<DisciplineResponseDTO?> AddAverages(DisciplineUpdateAveragesDTO discipline, string disciplineId);
    Task<DisciplineResponseDTO?> RemoveAverages(DisciplineUpdateAveragesDTO discipline, string disciplineId);
    Task<DisciplineDTO?> Delete(string disciplineId);
    Task<DisciplineResponseDTO?> GetById(string disciplineId);
}