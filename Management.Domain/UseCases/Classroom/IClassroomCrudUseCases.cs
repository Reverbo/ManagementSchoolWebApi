using Management.Domain.Domains.DTO.Classroom;

namespace Management.Domain.UseCases.Classroom;

public interface IClassroomCrudUseCases
{
    Task<ClassroomResponseDTO> Create(ClassroomDTO classroom);

    Task<ClassroomResponseDTO> Update(ClassroomUpdateDTO classroomDto, string classroomId);
    
    Task<ClassroomResponseDTO?> AddStudents(ClassroomUpdateStudentsDTO classroomDto, string classroomId);
    
    Task<ClassroomResponseDTO?> RemoveStudents(ClassroomUpdateStudentsDTO classroomDto, string classroomId);
    
    Task Delete(string classroomId);
    
    Task<ClassroomResponseDTO> GetById(string classroomId);
    
    Task<ClassroomResponseDTO> GetByName (string classroomName);
  
}