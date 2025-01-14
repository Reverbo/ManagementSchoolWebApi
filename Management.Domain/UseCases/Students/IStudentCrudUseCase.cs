using Management.Domain.Domains.DTO.Students;

namespace Management.Domain.UseCases.Students;

public interface IStudentCrudUseCase
{
    Task<StudentDTO> Create(StudentDTO student);
    
    Task<StudentDTO> GetById(string studentId);
    
    Task<StudentDTO> Update(StudentDTO student, string studentId);

    Task<StudentDTO> Delete(string studentId);
    
    Task<List<StudentDTO>> GetAll();
}