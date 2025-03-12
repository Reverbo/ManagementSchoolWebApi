using AutoMapper;
using Management.Domain.Domains.DTO.Students;
using Management.Domain.Gateway.Student;
using Management.Infrastructure.Database.Entities;
using MongoDB.Bson;
using MongoDB.Driver;


namespace Management.Infrastructure.Database.Repositories;

public class StudentRepository : IStudentReposityGateway
{
    private readonly IMongoCollection<StudentEntity> _students;
    private readonly IMapper _mapper;
    public StudentRepository(IMongoDatabase database, IMapper mapper)
    {
        _students = database.GetCollection<StudentEntity>("students");
        _mapper = mapper;
    }

    public async Task<StudentDTO> Create(StudentDTO student)
    {
        var studentEntity = _mapper.Map<StudentEntity>(student);
        studentEntity.Id = ObjectId.GenerateNewId();
        await _students.InsertOneAsync(studentEntity);
        return _mapper.Map<StudentDTO>(studentEntity);
    }

    public async Task<StudentDTO?> Update(StudentUpdateDTO student, string studentId)
    {
        var studentObjectId = new ObjectId(studentId);
        var studentEntity = await _students.Find(item => item.Id == studentObjectId).FirstOrDefaultAsync();

        if (studentEntity == null)
        {
            return null;
        }

        studentEntity.UpdateByStudentDto(student);
        

        var result = await _students.ReplaceOneAsync(item => item.Id == studentObjectId, studentEntity);

        if (!result.IsAcknowledged)
        {
            return null;
        }

        var updateStudent = await _students.Find(item => item.Id == studentObjectId).FirstOrDefaultAsync();
        return _mapper.Map<StudentDTO>(updateStudent);
    }

    public async Task<StudentDTO?> Delete(string studentId)
    {
        var studentObjectId = new ObjectId(studentId);
        var studentEntity = await _students.Find(student => student.Id == studentObjectId).FirstOrDefaultAsync();

        if (studentEntity == null)
        {
            return null;
        }

        await _students.DeleteOneAsync(item => item.Id == studentObjectId);

        return _mapper.Map<StudentDTO>(studentEntity);
    }
    public async Task<List<StudentDTO>> GetAll()
    {
        var students = await _students.Find(listStudents => true).ToListAsync();

        return _mapper.Map<List<StudentDTO>>(students).ToList();
    }
    public async Task<StudentDTO?> GetById(string studentId)
    {
        var studentObjectId = new ObjectId(studentId);
        var studentEntity = await _students.Find(student => student.Id == studentObjectId).FirstOrDefaultAsync();
        if (studentEntity == null)
        {
            return null;
        }

        return _mapper.Map<StudentDTO>(studentEntity);
    }
    
    public async Task<StudentDTO?> GetByCpf(string cpf)
    {
        var studentEntity = await _students.Find(student => student.Cpf.Equals(cpf)).FirstOrDefaultAsync();
        if (studentEntity == null)
        {
            return null;
        }

        return _mapper.Map<StudentDTO>(studentEntity);
    }
    
    public async Task<StudentDTO?> GetByRg(string rg)
    {
        var studentEntity = await _students.Find(student => student.Rg.Equals(rg)).FirstOrDefaultAsync();
        if (studentEntity == null)
        {
            return null;
        }

        return _mapper.Map<StudentDTO>(studentEntity);
    }
}