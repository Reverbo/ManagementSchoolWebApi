using Management.Domain.Domains.DTO.Teachers;
using Management.Domain.Domains.Exceptions;
using Management.Domain.Gateway.Teacher;
using Management.Domain.UseCases.Teachers;

namespace Management.Domain.Services;

public class TeacherCrudService : ITeacherCrudUseCase
{
    private readonly ITeacherRepositoryGateway _teacherRepositoryGateway;

    public TeacherCrudService(ITeacherRepositoryGateway teacherRepositoryGateway)
    {
        _teacherRepositoryGateway = teacherRepositoryGateway;
    }
    
    public async Task<TeacherDTO> Create(TeacherDTO teacher)
    {
        return await _teacherRepositoryGateway.Create(teacher);
    }

    public async Task<TeacherDTO> Update(TeacherDTO teacher, string teacherId)
    {
        var updatedTeacher = await _teacherRepositoryGateway.Update(teacher, teacherId);
        
        if (updatedTeacher == null)
        {
            throw new TeacherException(404, $"Teacher with ID {teacherId} not found.");
        }
        
        return updatedTeacher;
    }

    public async Task<TeacherDTO> Delete(string teacherId)
    {
        var existingTeacher = await _teacherRepositoryGateway.Delete(teacherId);

        if (existingTeacher == null)
        {
            throw new TeacherException(404, $"Teacher with ID {teacherId} not found.");
        }
        
        return existingTeacher;
    }

    public async Task<ICollection<TeacherDTO>> GetAll()
    {
        return await _teacherRepositoryGateway.GetAll();
    }

    public async Task<TeacherDTO> GetById(string teacherId)
    {
        var existingTeacher = await _teacherRepositoryGateway.GetById(teacherId);

        if (existingTeacher == null)
        {
            throw new TeacherException(404, $"Teacher with ID {teacherId} not found.");
        }
        
        return existingTeacher;
    }
}