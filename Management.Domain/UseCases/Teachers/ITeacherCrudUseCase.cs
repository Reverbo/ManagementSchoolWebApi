using Management.Domain.Domains.DTO.Teachers;

namespace Management.Domain.UseCases.Teachers;

public interface ITeacherCrudUseCase
{
    Task<TeacherDTO> Create(TeacherDTO teacher);
    Task<TeacherDTO> Update(TeacherUpdateDTO teacher, string teacherId);
    Task<TeacherDTO> Delete(string teacherId);
    Task<ICollection<TeacherDTO>> GetAll();
    Task<TeacherDTO> GetById(string teacherId);
}