using Management.Domain.Domains.DTO.Classroom;
using Management.Domain.Domains.Exceptions;
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

    public async Task<ClassroomResponseDTO> Create(ClassroomDTO classroomDto)
    {
        foreach (var itemClassroom in classroomDto.StudentsId)
        {
            var existingStudent = await _studentReposityGateway.GetById(itemClassroom) != null;

            if (!existingStudent)
            {
                throw new ClassroomException(404, "It is necessary that all students exist.");
            }
        }

        await ValidateNameAndDate(classroomDto.SchoolYear, classroomDto.ClassName);
        
        return await _classroomRepositoryGateway.Create(classroomDto);
    }

    public async Task<ClassroomResponseDTO> Update(ClassroomUpdateDTO classroomDto, string classroomId)
    {
        await ValidateNameAndDate(classroomDto.SchoolYear, classroomDto.ClassName);
        
        var existingClassroom = await _classroomRepositoryGateway.Update(classroomDto, classroomId);

        if (existingClassroom == null)
        {
            throw new ClassroomException(404, $"Classroom with ID {classroomId} not found.");
        }

        return existingClassroom;
    }

    public async Task<ClassroomResponseDTO?> AddStudents(ClassroomUpdateStudentsDTO classroomDto, string classroomId)
    {
        await ValidateStudentsExistence(classroomDto.StudentsId);

        var classroomGetIds = await GetClassroomStudentIds(classroomId);

        foreach (var studentId in classroomDto.StudentsId)
        {
            if (classroomGetIds.Contains(studentId))
            {
                throw new ClassroomException(400, $"Unable to add students. Student ID {studentId} has already been added.");
            }
        }
        
        var classroom = await _classroomRepositoryGateway.AddStudents(classroomDto, classroomId);

        return classroom;
    }

    public async Task<ClassroomResponseDTO?> RemoveStudents(ClassroomUpdateStudentsDTO classroomDto, string classroomId)
    {
        await ValidateStudentsExistence(classroomDto.StudentsId);
        
        var classroomGetIds = await GetClassroomStudentIds(classroomId);
        
        if (classroomGetIds.Count == 0)
        {
            throw new ClassroomException(404, $"Classroom with ID {classroomId} has not students.");
        }
        
        foreach (var studentId in classroomDto.StudentsId)
        {
            if (!classroomGetIds.Contains(studentId))
            {
                throw new ClassroomException(400, $"Unable to remove students. The student {studentId} could not be removed because he does not exist in this classroom.");
            }
        }
        
        var classroom = await _classroomRepositoryGateway.RemoveStudents(classroomDto, classroomId);
        
        return classroom;
    }

    public async Task Delete(string classroomId)
    {
        var existingClassroom = await _classroomRepositoryGateway.Delete(classroomId) != null;

        if (!existingClassroom)
        {
            throw new ClassroomException(404, $"Classroom with ID {classroomId} not found.");
        }
    }

    public async Task<ClassroomResponseDTO> GetById(string classroomId)
    {
        var classroom = await _classroomRepositoryGateway.GetById(classroomId);

        if (classroom == null)
        {
            throw new ClassroomException(404, $"Classroom with ID {classroomId} not found.");
        }

        return classroom;
    }

    public async Task<ClassroomResponseDTO> GetByName(string classroomName)
    {
        var classroomWithName = await _classroomRepositoryGateway.GetByName(classroomName);

        if (classroomWithName == null)
        {
            throw new ClassroomException(404, $"{classroomName} Classroom does not exist");
        }

        return classroomWithName;
    }

    private async Task ValidateStudentsExistence(List<string> studentIdList)
    {
        foreach (var itemStudentId in studentIdList)
        {
            var existingStudent = await _studentReposityGateway.GetById(itemStudentId);

            if (existingStudent == null)
            {
                throw new ClassroomException(404,
                    $"The following student ID do not exist: {itemStudentId}");
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

    private async Task ValidateNameAndDate(string schoolYear, string className)
    {
        var existingClassroomWithThisName = await _classroomRepositoryGateway.GetByName(className) != null;

        if (!existingClassroomWithThisName)
        {
            throw new ClassroomException(400, "It was not possible to create a new classroom because a classroom with this name already exists.");
        }

        var currentDate = DateTime.Now;

        var dateIsInvalid = !DateTime.TryParse(schoolYear, out DateTime dateClassroom) || 
                            dateClassroom.Year > (currentDate.Year + 1) || 
                            dateClassroom.Year < currentDate.Year;
        
        if (dateIsInvalid)
        {
            throw new ClassroomException(400, "Invalid date. Please provide a valid date within the current or next year.");
        }
    }
}