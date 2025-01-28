using Management.Domain.Domains.DTO.Classroom;
using Management.Domain.Domains.DTO.Students;

namespace Management.Domain.UseCases.Classroom;

public interface IClassroomCrudUseCases
{
    Task<ClassroomResponseDTO> Create(ClassroomDTO classroom);

    Task<ClassroomResponseDTO?> Update(ClassroomDTO classroomDto, string classroomId);
    Task<ClassroomResponseDTO?> AddStudent(ClassroomDTO classroomDto, string classroomId);
    
    Task<ClassroomResponseDTO?> RemoveStudent(ClassroomDTO classroomDto, string classroomId);
    
    Task<ClassroomResponseDTO?> Delete(string classroomId);
    Task<ClassroomResponseDTO?> GetById(string classroomId);
    
    Task<ClassroomResponseDTO> GetByName (string classroomName);
  
}