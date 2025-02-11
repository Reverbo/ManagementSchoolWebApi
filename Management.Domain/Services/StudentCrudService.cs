using Management.Domain.Domains.DTO.Students;
using Management.Domain.Domains.Exceptions;
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
    
    public async Task<StudentDTO> Update(StudentDTO student, string studentId)
    {
       await ValidateStudentExistence(studentId);
       
       return await _studentReposityGateway.Update(student, studentId);
    }

    public async Task<StudentDTO> Delete(string studentId)
    {
        await ValidateStudentExistence(studentId);
        
        return await _studentReposityGateway.Delete(studentId);
    }
    
    public async Task<List<StudentDTO>> GetAll()
    {
        return await _studentReposityGateway.GetAll();
    }
    public async Task<StudentDTO> GetById(string studentId)
    {
        await ValidateStudentExistence(studentId);
        
        return await _studentReposityGateway.GetById(studentId);
    }
    
    private async Task ValidateStudentExistence(string studentId)
    {
        var existingStudentId = await _studentReposityGateway.GetById(studentId);
        if (existingStudentId == null)
        { 
            throw new StudentException(404, $"Student with ID {studentId} not found.");
        }
}
    
}