using Management.Domain.Domains.DTO.Students;
using Management.Domain.Gateway.Student;
using Management.Domain.UseCases.Students;

namespace Management.Domain.Services;

public class StudentCrudService : IStudentCrudUseCase
{
    private readonly IStudentReposityGateway _studentReposityGateway;

    public StudentCrudService(IStudentReposityGateway studentReposityGateway)
    {
        _studentReposityGateway = studentReposityGateway;
    }

    public async Task<StudentDTO> Create(StudentDTO student)
    {
        return await _studentReposityGateway.Create(student);
    }
}