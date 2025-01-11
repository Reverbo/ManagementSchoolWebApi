using Management.Domain.Domains.DTO.Teachers;

namespace Management.Domain.UseCases.Teachers;

public interface ITeacherCrudUseCase
{
    Task<TeacherDTO> Create(TeacherDTO teacher);
    Task<TeacherDTO> Update(TeacherDTO teacher);
    Task<TeacherDTO> Delete(TeacherDTO teacher);
    Task<TeacherDTO> GetAll(TeacherDTO teacher);
    Task<TeacherDTO> GetById(TeacherDTO teacher);
}