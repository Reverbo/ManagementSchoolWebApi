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
    
    public async Task<TeacherDto> Create(TeacherDto teacher)
    {
        return await _teacherRepositoryGateway.Create(teacher);
    }

    public async Task<TeacherDto> Update(TeacherDto teacher, string teacherId)
    {
        var updated = await _teacherRepositoryGateway.Update(teacher, teacherId);
        
        if (updated == null)
        {
            throw new TeacherException(404, $"Teacher with ID {teacherId} not found.");
        }
        
        return updated;
    }

    public async Task<TeacherDto> Delete(string teacherId)
    {
        var existingTeacher = await _teacherRepositoryGateway.Delete(teacherId);

        if (existingTeacher == null)
        {
            throw new TeacherException(404, $"Teacher with ID {teacherId} not found.");
        }
        
        return existingTeacher;
    }

    public async Task<ICollection<TeacherDto>> GetAll()
    {
        return await _teacherRepositoryGateway.GetAll();
    }

    public async Task<TeacherDto> GetById(string teacherId)
    {
        var existingTeacher = await _teacherRepositoryGateway.GetById(teacherId);

        if (existingTeacher == null)
        {
            throw new TeacherException(404, $"Teacher with ID {teacherId} not found.");
        }
        
        return existingTeacher;
    }
}