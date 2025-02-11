using Management.Domain.Domains.DTO.Classroom;

namespace Management.Domain.Gateway.Classroom;

public interface IClassroomRepositoryGateway
{
    Task<ClassroomResponseDTO> Create(ClassroomDTO classroom);
    Task<ClassroomResponseDTO?> Update(ClassroomUpdateDTO classroom, string classroomId);
    Task<ClassroomResponseDTO?> AddStudents(ClassroomUpdateStudentsDTO classroomDto, string classroomId);
    Task<ClassroomResponseDTO?> RemoveStudents(ClassroomUpdateStudentsDTO classroomDto, string classroomId);
    Task<ClassroomResponseDTO?> Delete(string classroomId);
    Task<ClassroomResponseDTO?> GetById(string id);
    Task<ClassroomResponseDTO?> GetByName(string classroomName);
}