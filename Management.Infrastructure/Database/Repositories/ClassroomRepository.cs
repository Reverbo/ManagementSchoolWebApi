using AutoMapper;
using Management.Domain.Domains.DTO.Classroom;
using Management.Domain.Domains.DTO.Students;
using Management.Domain.Gateway;
using Management.Domain.Gateway.Classroom;
using Management.Domain.Gateway.Student;
using Management.Infrasctructure.Database.Entities;
using Management.Infrastructure.Database.Entities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Management.Infrastructure.Database.Repositories;

public class ClassroomRepository : IClassroomRepositoryGateway
{
    private readonly IMongoCollection<ClassroomEntity> _classrooms;
    private readonly IStudentReposityGateway _students;
    private readonly IMapper _mapper;

    public ClassroomRepository(IMongoDatabase database, IMapper mapper, IStudentReposityGateway students)
    {
        _classrooms = database.GetCollection<ClassroomEntity>("classrooms");
        _students = students;
        _mapper = mapper;
    }

    public async Task<ClassroomResponseDTO> Create(ClassroomDTO classroom)
    {
        var classroomEntity = _mapper.Map<ClassroomEntity>(classroom);
        classroomEntity.Id = ObjectId.GenerateNewId();
        await _classrooms.InsertOneAsync(classroomEntity);

        var studentList = await GetStudentList(classroom.StudentsId);
        
        var classroomResponse = _mapper.Map<ClassroomResponseEntity>(classroomEntity);
        classroomResponse.Students = studentList;

        return _mapper.Map<ClassroomResponseDTO>(classroomResponse);
    }

    public async Task<ClassroomResponseDTO?> Update(ClassroomUpdateDTO classroom, string classroomId)
    {
        var objectId = new ObjectId(classroomId);

        var existingClassroom = await _classrooms.Find(item => item.Id == objectId).FirstOrDefaultAsync();

        if (existingClassroom == null)
        {
            return null;
        }

        existingClassroom.ClassName = classroom.ClassName;
        existingClassroom.SchoolYear = classroom.SchoolYear;

        var result = await _classrooms.ReplaceOneAsync(item => item.Id == objectId, existingClassroom);

        if (!result.IsAcknowledged)
        {
            return null;
        }

        var updatedClassroom = await _classrooms.Find(item => item.Id == objectId).FirstOrDefaultAsync();

        var studentList = await GetStudentList(updatedClassroom.StudentsId);

        var classroomResponse = _mapper.Map<ClassroomResponseEntity>(existingClassroom);
        classroomResponse.Students = studentList;

        return _mapper.Map<ClassroomResponseDTO>(classroomResponse);
    }

    public async Task<ClassroomResponseDTO?> AddStudents(ClassroomUpdateStudentsDTO classroomDto, string classroomId)
    {
        var objectId = new ObjectId(classroomId);
        var classroom = await _classrooms.Find(item => item.Id == objectId).FirstOrDefaultAsync();

        if (classroom == null)
        {
            return null;
        }
        
        foreach (var studentId in classroomDto.StudentsId)
        {
            classroom.StudentsId.Add(studentId);
        }
        
        var studentList = await GetStudentList(classroom.StudentsId);

        var classroomResponse = _mapper.Map<ClassroomResponseEntity>(classroom);
        classroomResponse.Students = studentList;

        var result = await _classrooms.ReplaceOneAsync(item => item.Id == objectId, classroom);

        if (!result.IsAcknowledged)
        {
            return null;
        }

        return _mapper.Map<ClassroomResponseDTO>(classroomResponse);
    }

    public async Task<ClassroomResponseDTO?> RemoveStudents(ClassroomUpdateStudentsDTO classroomDto, string classroomId)
    {
        var objectId = new ObjectId(classroomId);
        var classroom = await _classrooms.Find(item => item.Id == objectId).FirstOrDefaultAsync();

        if (classroom == null)
        {
            return null;
        }
        
        foreach (var studentId in classroomDto.StudentsId)
        {
            classroom.StudentsId.Remove(studentId);
        }

        var studentList = await GetStudentList(classroom.StudentsId);

        var classroomResponse = _mapper.Map<ClassroomResponseEntity>(classroom);
        classroomResponse.Students = studentList;
        var result = await _classrooms.ReplaceOneAsync(item => item.Id == objectId, classroom);

        if (!result.IsAcknowledged)
        {
            return null;
        }

        return _mapper.Map<ClassroomResponseDTO>(classroomResponse);
    }

    public async Task<ClassroomResponseDTO?> Delete(string classroomId)
    {
        var objectId = new ObjectId(classroomId);
        var existingClassroom = await _classrooms.Find(item => item.Id == objectId).FirstOrDefaultAsync();

        if (existingClassroom == null)
        {
            return null;
        }

        await _classrooms.DeleteOneAsync(item => item.Id == objectId);

        return _mapper.Map<ClassroomResponseDTO>(existingClassroom);
    }

    public async Task<ClassroomResponseDTO?> GetById(string classroomId)
    {
        var objectId = new ObjectId(classroomId);
        var classroom = await _classrooms.Find(item => item.Id == objectId).FirstOrDefaultAsync();


        if (classroom == null)
        {
            return null;
        }

        var studentList = await GetStudentList(classroom.StudentsId);

        var classroomResponse = _mapper.Map<ClassroomResponseEntity>(classroom);
        classroomResponse.Students = studentList;
        return _mapper.Map<ClassroomResponseDTO>(classroomResponse);
    }

    public async Task<ClassroomResponseDTO?> GetByName(string classroomName)
    {
        var classroom = await _classrooms.Find(item => item.ClassName.ToLower() == classroomName.ToLower())
            .FirstOrDefaultAsync();

        if (classroom == null)
        {
            return null;
        }

        var studentList = await GetStudentList(classroom.StudentsId);

        var classroomResponse = _mapper.Map<ClassroomResponseEntity>(classroom);
        classroomResponse.Students = studentList;
        return _mapper.Map<ClassroomResponseDTO>(classroomResponse);
    }
    
    private async Task<List<StudentEntity>> GetStudentList(List<string> studentsIds)
    {
        var students = new List<StudentDTO>();
        foreach (var studentId in studentsIds)
        {
            var studentGetById = await _students.GetById(studentId);
            if (studentGetById != null)
            {
                students.Add(studentGetById);
            }
        }

        return _mapper.Map<List<StudentEntity>>(students);
    }
}