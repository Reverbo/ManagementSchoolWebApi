using AutoMapper;
using Management.Domain.Domains.DTO.Students;
using Management.Domain.Gateway.Student;
using Management.Infrasctructure.Database.Entities;
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

    public async Task<StudentDTO> GetById(string studentId)
    {
        var id = new ObjectId(studentId);
        var student = await _students.Find(student => student.Id == id).FirstOrDefaultAsync();
        if (student == null)
        {
            return null;
        }
        
        return _mapper.Map<StudentDTO>(student);;
    }
}