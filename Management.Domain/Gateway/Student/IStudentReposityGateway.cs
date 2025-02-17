using Management.Domain.Domains.DTO.Students;

namespace Management.Domain.Gateway.Student;

public interface IStudentReposityGateway
{
    Task<StudentDTO> Create(StudentDTO student);
    Task<StudentDTO?> Update(StudentUpdateDTO student, string studentId);
    Task<StudentDTO?> Delete(string studentId);
    Task<List<StudentDTO>> GetAll();
    Task<StudentDTO?> GetById(string studentId);
    Task<StudentDTO?> GetByCpf(string cpf);
    Task<StudentDTO?> GetByRg(string cpf);
}