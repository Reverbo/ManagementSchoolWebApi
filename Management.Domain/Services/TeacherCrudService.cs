using Management.Domain.Domains.DTO.Teachers;
using Management.Domain.Domains.Exceptions;
using Management.Domain.Domains.Exceptions.Teacher;
using Management.Domain.Gateway;
using Management.Domain.Gateway.Classroom;
using Management.Domain.Gateway.Teacher;
using Management.Domain.UseCases.Teachers;

namespace Management.Domain.Services;

public class TeacherCrudService : ITeacherCrudUseCase
{
    private readonly ITeacherRepositoryGateway _teacherRepositoryGateway;
    private readonly IDisciplineRepositoryGateway _disciplineRepositoryGateway;
    private readonly IClassroomRepositoryGateway _classroomRepositoryGateway;
    public TeacherCrudService(ITeacherRepositoryGateway teacherRepositoryGateway, 
        IDisciplineRepositoryGateway disciplineRepositoryGateway, 
        IClassroomRepositoryGateway classroomRepositoryGateway)
    {
        _teacherRepositoryGateway = teacherRepositoryGateway;
        _disciplineRepositoryGateway = disciplineRepositoryGateway;
        _classroomRepositoryGateway = classroomRepositoryGateway;
    }
    
    public async Task<TeacherDTO> Create(TeacherDTO teacher)
    {
        
        var dateIsInvalid = !DateTime.TryParse(teacher.DateBirth, out var dateBirthConvert);
        
        if (dateIsInvalid)
        {
            throw new TeacherInvalidDateException(404, "date provided is invalid");
        }
        
        if (dateBirthConvert > DateTime.Now.AddYears(-18))
        {
            throw new TeacherInvalidDateException(404, $"Age must be over 18 years old");
        }
        
       
        var teacherCpf = await _teacherRepositoryGateway.GetByCpf(teacher.Cpf) != null;

        if (teacherCpf)
        {
            throw new TeacherNotFoundException(404, "Teacher with CPF already exists.");
        }

        var existingDiscipline = await _disciplineRepositoryGateway.GetById(teacher.DisciplineId) != null;
        if (!existingDiscipline)
        {
            throw new DisciplineNotFoundException(404, $"Discipline with ID {teacher.DisciplineId} not found.");
        }
        
        var existingClassoom = await _classroomRepositoryGateway.GetById(teacher.ClassroomId) != null;
        if (!existingClassoom)
        {
            throw new ClassroomNotFoundException(404, $"Classroom with ID {teacher.ClassroomId} not found.");
        }
        
        return await _teacherRepositoryGateway.Create(teacher);
    }

    public async Task<TeacherDTO> Update(TeacherUpdateDTO teacher, string teacherId)
    {
        var updatedTeacher = await _teacherRepositoryGateway.Update(teacher, teacherId);
        
        if (updatedTeacher == null)
        {
            throw new TeacherNotFoundException(404, $"Teacher with ID {teacherId} not found.");
        }
        
        var existingDiscipline = await _disciplineRepositoryGateway.GetById(teacher.DisciplineId) != null;
        if (!existingDiscipline)
        {
            throw new DisciplineNotFoundException(404, $"Discipline with ID {teacher.DisciplineId} not found.");
        }
        
        var existingClassoom = await _classroomRepositoryGateway.GetById(teacher.ClassroomId) != null;
        if (!existingClassoom)
        {
            throw new ClassroomNotFoundException(404, $"Classroom with ID {teacher.ClassroomId} not found.");
        }
        return updatedTeacher;
    }

    public async Task<TeacherDTO> Delete(string teacherId)
    {
        var existingTeacher = await _teacherRepositoryGateway.Delete(teacherId);

        if (existingTeacher == null)
        {
            throw new TeacherNotFoundException(404, $"Teacher with ID {teacherId} not found.");
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
            throw new TeacherNotFoundException(404, $"Teacher with ID {teacherId} not found.");
        }
        
        return existingTeacher;
    }

}