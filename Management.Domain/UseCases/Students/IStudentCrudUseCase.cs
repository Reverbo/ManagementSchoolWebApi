using Management.Domain.Domains.DTO.Students;

namespace Management.Domain.UseCases.Students;

public interface IStudentCrudUseCase
{
    Task<StudentDTO> Create(StudentDTO student);
}