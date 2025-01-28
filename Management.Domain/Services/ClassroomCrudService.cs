using Management.Domain.Domains.DTO.Classroom;
using Management.Domain.Domains.DTO.Students;
using Management.Domain.Domains.Exceptions;
using Management.Domain.Gateway;
using Management.Domain.Gateway.Classroom;
using Management.Domain.Gateway.Student;
using Management.Domain.UseCases.Classroom;

namespace Management.Domain.Services;

public class ClassroomCrudService : IClassroomCrudUseCases
{
    private readonly IClassroomRepositoryGateway _classroomRepositoryGateway;
    private readonly IStudentReposityGateway _studentReposityGateway;

    public ClassroomCrudService(IClassroomRepositoryGateway classroomRepositoryGateway, IStudentReposityGateway studentReposityGateway)
    {
        _classroomRepositoryGateway = classroomRepositoryGateway;
        _studentReposityGateway = studentReposityGateway;
    }
    
    public async Task<ClassroomResponseDTO> Create(ClassroomDTO classroom)
    {
        
        foreach (var itemClassroom in classroom.StudentsId)
        {
            var existingStudent = await _studentReposityGateway.GetById(itemClassroom);

            if (existingStudent == null)
            {
                throw new ClassroomException(404, "It is necessary that all students exist.");
            }
        }
        
        return await _classroomRepositoryGateway.Create(classroom);;
    }
    public async Task<ClassroomResponseDTO?> Update(ClassroomDTO classroom, string classroomId)
    {
       await ValidateClassroomExistence(classroomId);
       
        return await _classroomRepositoryGateway.Update(classroom, classroomId);
    }

    public async Task<ClassroomResponseDTO?> AddStudent (ClassroomDTO classroom, string classroomId)
    {
        await ValidateStudentsExistence(classroom);
        
        return await _classroomRepositoryGateway.AddStudent(classroom, classroomId);;
    }

    public async Task<ClassroomResponseDTO?> RemoveStudent(ClassroomDTO classroom, string classroomId)
    {
        await ValidateStudentsExistence(classroom);
        
        return await _classroomRepositoryGateway.RemoveStudent(classroom, classroomId);;
    }

    public async Task<ClassroomResponseDTO?> Delete(string classroomId)
    {
        await ValidateClassroomExistence(classroomId);
        
        return await _classroomRepositoryGateway.Delete(classroomId);;
    }

    public async Task<ClassroomResponseDTO?> GetById(string classroomId)
    {
        await ValidateClassroomExistence(classroomId);
        return await _classroomRepositoryGateway.GetById(classroomId);;
        
    }

    public async Task<ClassroomResponseDTO> GetByName(string classroomName)
    {
        var existingName = await _classroomRepositoryGateway.GetByName(classroomName);

        if (existingName == null)
        {
            throw new ClassroomException(404, $"{classroomName} Classroom does not exist");
        }

        return existingName;
    }
    
    private async Task ValidateStudentsExistence (ClassroomDTO classroom)
    {
        foreach (var itemClassroomId in classroom.StudentsId)
        {
            var existingStudent = await _studentReposityGateway.GetById(itemClassroomId);

            if (existingStudent == null)
            {
                throw new ClassroomException(404,
                    $"The following student IDs do not exist: {string.Join(", ", classroom.StudentsId)}");
            }
        }
        
    }
    private async Task <string> ValidateClassroomExistence(string classroomId)
    {
        var existingClassroom = await _classroomRepositoryGateway.GetById(classroomId);
        if (existingClassroom == null)
        { 
            throw new ClassroomException(404, $"Classroom with ID {classroomId} not found.");
        }
        return String.Empty;
    }

}