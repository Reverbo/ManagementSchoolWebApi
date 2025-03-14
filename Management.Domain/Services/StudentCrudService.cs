using Management.Domain.Domains.DTO.Students;
using Management.Domain.Domains.Exceptions;
using Management.Domain.Gateway.Classroom;
using Management.Domain.Gateway.Student;
using Management.Domain.UseCases.Students;

namespace Management.Domain.Services;

public class StudentCrudService : IStudentCrudUseCase
{
    private readonly IStudentReposityGateway _studentReposityGateway;
    private readonly IClassroomRepositoryGateway _classroomReposityGateway;

    public StudentCrudService(IStudentReposityGateway studentReposityGateway,
        IClassroomRepositoryGateway classroomReposityGateway)
    {
        _studentReposityGateway = studentReposityGateway;
        _classroomReposityGateway = classroomReposityGateway;
    }

    public async Task<StudentDTO> Create(StudentDTO studentDto)
    {
        var dateIsInvalid = !DateTime.TryParse(studentDto.DateBirth, out var dateBirthConvert);
        if (dateIsInvalid)
        {
            throw new StudentInvalidDateException(404, "date provided is invalid");
        }

        if (dateBirthConvert >= DateTime.Now)
        {
            throw new StudentInvalidDateException(404,
                $"The provided birth date is invalid as it cannot be later than today date.");
        }

        var existingClassroom = await _classroomReposityGateway.GetById(studentDto.ClassroomId) != null;
        if (!existingClassroom)
        {
            throw new ClassroomNotFoundException(404, $"Classroom with ID {studentDto.ClassroomId} not found.");
        }


        await VerifyDocumentsIsUnique(studentDto);

        return await _studentReposityGateway.Create(studentDto);
    }

    public async Task<StudentDTO> Update(StudentUpdateDTO studentDto, string studentId)
    {
        await ValidateStudentExistence(studentId);
        var existingClassroomId = await _classroomReposityGateway.GetById(studentDto.ClassroomId) != null;
        if (!existingClassroomId)
        {
            throw new ClassroomNotFoundException(404, $"Classroom with ID {studentDto.ClassroomId} not found.");
        }

        var updateStudent = await _studentReposityGateway.Update(studentDto, studentId);
        return updateStudent!;
    }

    public async Task<StudentDTO> Delete(string studentId)
    {
        await ValidateStudentExistence(studentId);

        var deleteStudent = await _studentReposityGateway.Delete(studentId);

        return deleteStudent!;
    }

    public async Task<List<StudentDTO>> GetAll()
    {
        return await _studentReposityGateway.GetAll();
    }

    public async Task<StudentDTO> GetById(string studentId)
    {
        await ValidateStudentExistence(studentId);

        var getStudent = await _studentReposityGateway.GetById(studentId);
        return getStudent!;
    }

    private async Task ValidateStudentExistence(string studentId)
    {
        var existingStudentId = await _studentReposityGateway.GetById(studentId) != null;
        if (!existingStudentId)
        {
            throw new StudentNotFoundException(404, $"Student with ID {studentId} not found.");
        }
    }

    private async Task VerifyDocumentsIsUnique(StudentDTO student)
    {
        var rgAlreadyExist = await _studentReposityGateway.GetByRg(student.Rg) != null;
        var cpfAlreadyExist = await _studentReposityGateway.GetByCpf(student.Cpf) != null;

        if (cpfAlreadyExist)
        {
            throw new StudentDocumentException(404, "Student with CPF already exists.");
        }

        if (rgAlreadyExist)
        {
            throw new StudentDocumentException(404, "Student with RG already exists.");
        }
    }
}