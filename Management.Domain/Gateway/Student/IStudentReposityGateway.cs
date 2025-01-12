using Management.Domain.Domains.DTO.Students;

namespace Management.Domain.Gateway.Student;

public interface IStudentReposityGateway
{
    Task<StudentDTO> Create(StudentDTO student);
    
    Task<StudentDTO> GetById(string studentId);
}