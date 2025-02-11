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
    private readonly IMongoCollection<StudentEntity> _students;
    private readonly IMapper _mapper;

    public ClassroomRepository(IMongoDatabase database, IMapper mapper)
    {
        _classrooms = database.GetCollection<ClassroomEntity>("classrooms");
        _students = database.GetCollection<StudentEntity>("students");
        _mapper = mapper;
    }

    public async Task<ClassroomResponseDTO> Create(ClassroomDTO classroom)
    {
        var classroomEntity = _mapper.Map<ClassroomEntity>(classroom);
        classroomEntity.Id = ObjectId.GenerateNewId();
        await _classrooms.InsertOneAsync(classroomEntity);

        var studentEntityList = classroom.StudentsId.Select(itemId =>
        {
            var studentObjectId = new ObjectId(itemId);
            var studentEntity = _students.Find(itemStudent => itemStudent.Id == studentObjectId).FirstOrDefault();
            return _mapper.Map<StudentEntity>(studentEntity);
        }).ToList();

        var classroomResponse = _mapper.Map<ClassroomResponseEntity>(classroomEntity);
        classroomResponse.Students = studentEntityList;

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
        existingClassroom.BimonthlyId = classroom.BimonthlyId;

        var result = await _classrooms.ReplaceOneAsync(item => item.Id == objectId, existingClassroom);

        if (!result.IsAcknowledged)
        {
            return null;
        }

        var updatedClassroom = await _classrooms.Find(item => item.Id == objectId).FirstOrDefaultAsync();

        var studentEntityList = updatedClassroom.StudentsId.Select(itemId =>
        {
            var studentObjectId = new ObjectId(itemId);
            var studentEntity = _students.Find(itemStudent => itemStudent.Id == studentObjectId).FirstOrDefault();
            return studentEntity;
        }).ToList();

        var classroomResponse = _mapper.Map<ClassroomResponseEntity>(existingClassroom);
        classroomResponse.Students = studentEntityList;

        return _mapper.Map<ClassroomResponseDTO>(classroomResponse);
    }

    public async Task<ClassroomResponseDTO?> AddStudents(ClassroomUpdateStudentsDTO classroomDto, string classroomId)
    {
        var objectId = new ObjectId(classroomId);
        var existingClassroom = await _classrooms.Find(item => item.Id == objectId).FirstOrDefaultAsync();

        if (existingClassroom == null)
        {
            return null;
        }

        var studentEntityList = existingClassroom.StudentsId.Select(itemId =>
        {
            var studentObjectId = new ObjectId(itemId);
            var studentEntity = _students.Find(itemStudent => itemStudent.Id == studentObjectId).FirstOrDefault();
            return studentEntity;
        }).ToList();


        foreach (var studentId in classroomDto.StudentsId)
        {
            existingClassroom.StudentsId.Add(studentId);
        }

        var classroomResponse = _mapper.Map<ClassroomResponseEntity>(existingClassroom);
        classroomResponse.Students = studentEntityList;

        var result = await _classrooms.ReplaceOneAsync(item => item.Id == objectId, existingClassroom);

        if (!result.IsAcknowledged)
        {
            return null;
        }

        return _mapper.Map<ClassroomResponseDTO>(classroomResponse);
    }

    public async Task<ClassroomResponseDTO?> RemoveStudents(ClassroomUpdateStudentsDTO classroomDto, string classroomId)
    {
        var objectId = new ObjectId(classroomId);
        var existingClassroom = await _classrooms.Find(item => item.Id == objectId).FirstOrDefaultAsync();

        if (existingClassroom == null)
        {
            return null;
        }

        var studentEntityList = existingClassroom.StudentsId.Select(itemId =>
        {
            var studentObjectId = new ObjectId(itemId);
            var studentEntity = _students.Find(itemStudent => itemStudent.Id == studentObjectId).FirstOrDefault();
            return studentEntity;
        }).ToList();


        foreach (var studentId in classroomDto.StudentsId)
        {
            existingClassroom.StudentsId.Remove(studentId);
        }

        var classroomResponse = _mapper.Map<ClassroomResponseEntity>(existingClassroom);
        classroomResponse.Students = studentEntityList;
        var result = await _classrooms.ReplaceOneAsync(item => item.Id == objectId, existingClassroom);

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

        var studentIds = classroom.StudentsId.Select(itemId => new ObjectId(itemId)).ToList();
        var students = await _students.Find(student => studentIds.Contains(student.Id)).ToListAsync();

        var classroomResponse = _mapper.Map<ClassroomResponseEntity>(classroom);
        classroomResponse.Students = students;
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

        var studentList = classroom.StudentsId.Select(itemId =>
        {
            var studentObjectId = new ObjectId(itemId);
            var studentEntity = _students.Find(itemStudent => itemStudent.Id == studentObjectId).FirstOrDefault();
            return studentEntity;
        }).ToList();

        var classroomResponse = _mapper.Map<ClassroomResponseEntity>(classroom);
        classroomResponse.Students = studentList;
        return _mapper.Map<ClassroomResponseDTO>(classroomResponse);
    }
}