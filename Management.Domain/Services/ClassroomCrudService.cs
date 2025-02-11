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

    public ClassroomCrudService(IClassroomRepositoryGateway classroomRepositoryGateway,
        IStudentReposityGateway studentReposityGateway)
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

        var existingClassroomWithThisName = await _classroomRepositoryGateway.GetByName(classroom.ClassName);

        if (existingClassroomWithThisName != null)
        {
            throw new ClassroomException(400, "It was not possible to create a new classroom because a classroom with this name already exists.");
        }

        var currentDate = DateTime.Now;

        var dateIsInvalid = !DateTime.TryParse(classroom.SchoolYear, out DateTime dateClassroom) || 
            dateClassroom.Year > (currentDate.Year + 1) || 
            dateClassroom.Year < currentDate.Year;
        
        if (dateIsInvalid)
        {
            throw new ClassroomException(400, "Invalid date. Please provide a valid date within the current or next year.");
        }
        
        return await _classroomRepositoryGateway.Create(classroom);
    }

    public async Task<ClassroomResponseDTO> Update(ClassroomUpdateDTO classroom, string classroomId)
    {
        // fazer regras de negocios como no de create
        var existingClassroom = await _classroomRepositoryGateway.Update(classroom, classroomId);

        if (existingClassroom == null)
        {
            throw new ClassroomException(404, $"Classroom with ID {classroomId} not found.");
        }

        return existingClassroom;
    }

    public async Task<ClassroomResponseDTO?> AddStudents(ClassroomUpdateStudentsDTO classroom, string classroomId)
    {
        await ValidateStudentsExistence(classroom.StudentsId);

        var classroomGetIds = await GetClassroomStudentIds(classroomId);

        foreach (var studentId in classroom.StudentsId)
        {
            if (classroomGetIds.Contains(studentId))
            {
                throw new ClassroomException(400, $"Unable to add students. Student ID {studentId} has already been added.");
            }
        }
        
        var existingClassroom = await _classroomRepositoryGateway.AddStudents(classroom, classroomId);

        return existingClassroom;
    }

    public async Task<ClassroomResponseDTO?> RemoveStudents(ClassroomUpdateStudentsDTO classroom, string classroomId)
    {
        await ValidateStudentsExistence(classroom.StudentsId);
        
        var classroomGetIds = await GetClassroomStudentIds(classroomId);
        
        if (classroomGetIds.Count == 0)
        {
            throw new ClassroomException(404, $"Classroom with ID {classroomId} has not students.");
        }
        
        foreach (var studentId in classroom.StudentsId)
        {
            if (!classroomGetIds.Contains(studentId))
            {
                throw new ClassroomException(400, $"Unable to remove students. The student {studentId} could not be removed because he does not exist in this classroom.");
            }
        }
        
        var existingClassroom = await _classroomRepositoryGateway.RemoveStudents(classroom, classroomId);
        
        return existingClassroom;
    }

    public async Task Delete(string classroomId)
    {
        var existingClassroom = await _classroomRepositoryGateway.Delete(classroomId);

        if (existingClassroom == null)
        {
            throw new ClassroomException(404, $"Classroom with ID {classroomId} not found.");
        }
    }

    public async Task<ClassroomResponseDTO> GetById(string classroomId)
    {
        var existingClassroom = await _classroomRepositoryGateway.GetById(classroomId);

        if (existingClassroom == null)
        {
            throw new ClassroomException(404, $"Classroom with ID {classroomId} not found.");
        }

        return existingClassroom;
    }

    public async Task<ClassroomResponseDTO> GetByName(string classroomName)
    {
        var existingClassroomName = await _classroomRepositoryGateway.GetByName(classroomName);

        if (existingClassroomName == null)
        {
            throw new ClassroomException(404, $"{classroomName} Classroom does not exist");
        }

        return existingClassroomName;
    }

    private async Task ValidateStudentsExistence(List<string> studentIdList)
    {
        foreach (var itemStudentId in studentIdList)
        {
            var existingStudent = await _studentReposityGateway.GetById(itemStudentId);

            if (existingStudent == null)
            {
                throw new ClassroomException(404,
                    $"The following student IDs do not exist: {string.Join(", ", itemStudentId)}");
            }
        }
    }
    
    private async Task<List<string>> GetClassroomStudentIds(string classroomId)
    {
        var classroomGetById = await _classroomRepositoryGateway.GetById(classroomId);
        
        if (classroomGetById == null)
        {
            throw new ClassroomException(404, $"Classroom with ID {classroomId} not found.");
        }
        
        var classroomGetIds = classroomGetById.Students.Select(student => student.Id.ToString()).ToList() ?? [];

        return classroomGetIds;
    }
    
}