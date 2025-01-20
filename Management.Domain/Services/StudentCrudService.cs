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
       var updateStudent =  await _studentReposityGateway.Update(student, studentId);

       if (updateStudent == null)
       { 
           throw new StudentException(404, $"Teacher with ID {studentId} not found.");
       }
       
       return updateStudent;
    }

    public async Task<StudentDTO> Delete(string studentId)
    {
        var existingStudent = await _studentReposityGateway.Delete(studentId);

        if (existingStudent == null)
        { 
            throw new StudentException(404, $"Teacher with ID {studentId} not found.");
        }
        
        return existingStudent;
    }
    
    public async Task<List<StudentDTO>> GetAll()
    {
        return await _studentReposityGateway.GetAll();
    }
    public async Task<StudentDTO> GetById(string studentId)
    {
        var existingStudent=  await _studentReposityGateway.GetById(studentId);

        if (existingStudent == null)
        {
            throw new StudentException(404, $"Teacher with ID {studentId} not found.");
        }
        
        return existingStudent;
    }
}