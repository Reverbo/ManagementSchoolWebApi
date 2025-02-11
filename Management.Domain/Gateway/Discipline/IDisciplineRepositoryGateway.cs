using Management.Domain.Domains.DTO.Discipline;

namespace Management.Domain.Gateway;

public interface IDisciplineRepositoryGateway
{
    Task<DisciplineResponseDTO> Create(DisciplineCreateDTO discipline);
    Task<DisciplineResponseDTO?> Update(DisciplineEditDTO discipline, string disciplineId);
    Task<DisciplineResponseDTO?> AddAverages(DisciplineUpdateDTO discipline, string disciplineId);
    Task<DisciplineResponseDTO?> RemoveAverages(DisciplineUpdateDTO discipline, string disciplineId);
    Task<DisciplineDTO?> Delete(string disciplineId);
    Task<DisciplineDTO?> GetById(string disciplineId);
}