using Management.Domain.Domains.DTO.Teachers;

namespace Management.Domain.Gateway.Teacher;

public interface ITeacherRepositoryGateway
{
    Task<TeacherDto> Create(TeacherDto teacher);
    Task<TeacherDto?> Update(TeacherDto teacher, string teacherId);
    Task<TeacherDto?> Delete(String teacherId);
    Task<List<TeacherDto>> GetAll();
    Task<TeacherDto?> GetById(string teacherId);
}