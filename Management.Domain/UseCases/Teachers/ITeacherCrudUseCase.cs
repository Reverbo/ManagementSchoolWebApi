using Management.Domain.Domains.DTO.Teachers;

namespace Management.Domain.UseCases.Teachers;

public interface ITeacherCrudUseCase
{
    Task<TeacherDto> Create(TeacherDto teacher);
    Task<TeacherDto> Update(TeacherDto teacher, String teacherId);
    Task<TeacherDto> Delete(String teacherId);
    Task<ICollection<TeacherDto>> GetAll();
    Task<TeacherDto> GetById(String teacherId);
}