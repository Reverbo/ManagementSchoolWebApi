using Management.Domain.Domains.DTO.Classroom;

namespace Management.Domain.Gateway.Classroom;

public interface IClassroomRepositoryGateway
{
    Task<ClassroomResponseDTO> Create(ClassroomDTO classroom);
    Task<ClassroomResponseDTO?> Update(ClassroomDTO classroom, string classroomId);
    Task<ClassroomResponseDTO?> AddStudent(ClassroomDTO classroomDto, string classroomId);
    Task<ClassroomResponseDTO?> RemoveStudent(ClassroomDTO classroomDto, string classroomId);
    Task<ClassroomResponseDTO?> Delete(string classroomId);
    Task<ClassroomResponseDTO?> GetById(string id);
    Task<ClassroomResponseDTO?> GetByName(string classroomName);
}