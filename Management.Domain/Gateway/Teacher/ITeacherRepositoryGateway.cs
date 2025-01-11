using Management.Domain.Domains.DTO.Teachers;

namespace Management.Domain.Gateway.Teacher;

public interface ITeacherRepositoryGateway
{
    Task<TeacherDTO> Create(TeacherDTO teacher);
    Task<TeacherDTO?> Update(TeacherDTO teacher);
    void Delete(TeacherDTO teacher);
    Task<TeacherDTO[]?> GetAll(TeacherDTO teacher);
    Task<TeacherDTO?> GetById(TeacherDTO teacher);
}