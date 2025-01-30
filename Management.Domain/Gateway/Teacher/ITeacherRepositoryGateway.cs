using Management.Domain.Domains.DTO.Teachers;

namespace Management.Domain.Gateway.Teacher;

public interface ITeacherRepositoryGateway
{
    Task<TeacherDTO> Create(TeacherDTO teacher);
    Task<TeacherDTO?> Update(TeacherDTO teacher, string teacherId);
    Task<TeacherDTO?> Delete(string teacherId);
    Task<List<TeacherDTO>> GetAll();
    Task<TeacherDTO?> GetById(string teacherId);
}