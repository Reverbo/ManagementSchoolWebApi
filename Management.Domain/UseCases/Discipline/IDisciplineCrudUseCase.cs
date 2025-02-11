using Management.Domain.Domains.DTO.Discipline;

namespace Management.Domain.UseCases.Discipline;

public interface IDisciplineCrudUseCase
{
    Task<DisciplineResponseDTO> Create(DisciplineCreateDTO discipline);
    Task<DisciplineResponseDTO> Update(DisciplineUpdateDTO discipline, string disciplineId);
    Task<DisciplineResponseDTO> AddAverages(DisciplineUpdateAveragesDTO discipline, string disciplineId);
    Task<DisciplineResponseDTO> RemoveAverages(DisciplineUpdateAveragesDTO discipline, string disciplineId);
    Task<DisciplineDTO> Delete(string disciplineId);
    Task<DisciplineDTO> GetById(string disciplineId);
}