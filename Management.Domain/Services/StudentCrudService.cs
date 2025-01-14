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

    public async Task<StudentDTO> GetById(string studentId)
    {
        var getId =  await _studentReposityGateway.GetById(studentId);

        if (getId == null)
        {
            throw new StudentException(404, $"Teacher with ID {studentId} not found.");
        }
        
        return getId;
    }

    public async Task<StudentDTO> Update(StudentDTO student, string studentId)
    {
       var updateId =  await _studentReposityGateway.Update(student, studentId);

       if (updateId == null)
       { 
           throw new StudentException(404, $"Teacher with ID {studentId} not found.");
       }
       
       return updateId;
    }

    public async Task<StudentDTO> Delete(string studentId)
    {
        var deleteId = await _studentReposityGateway.Delete(studentId);

        if (deleteId == null)
        { 
            throw new StudentException(404, $"Teacher with ID {studentId} not found.");
        }
        
        return deleteId;
    }

    public async Task<List<StudentDTO>> GetAll()
    {
        return await _studentReposityGateway.GetAll();
    }
    
}