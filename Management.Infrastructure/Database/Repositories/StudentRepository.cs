using AutoMapper;
using Management.Domain.Domains.DTO.Students;
using Management.Domain.Gateway.Student;
using Management.Infrasctructure.Database.Entities;
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
    
    public async Task<StudentDTO> Update(StudentDTO student, string studentId)
    {
        var objectId = new ObjectId(studentId);
        var existingStudent = await _students.Find(student => student.Id == objectId).FirstOrDefaultAsync();

        if (existingStudent == null )
        {
            return null;
        }
        
        existingStudent.FullName = student.FullName;
        existingStudent.SocialName = student.SocialName;
        existingStudent.Age = student.Age;
        existingStudent.DocumentNumber = student.DocumentNumber;
        existingStudent.DocumentType = student.DocumentType;
        existingStudent.Email = student.Email;
        existingStudent.FatherName = student.FatherName;
        existingStudent.MotherName = student.MotherName;
        
        var result = await _students.ReplaceOneAsync(item => item.Id == objectId, existingStudent);
        
        if (!result.IsAcknowledged)
        {
            return null;
        }
        var updateStudent = await _students.Find(item => item.Id == objectId).FirstOrDefaultAsync();
        return _mapper.Map<StudentDTO>(updateStudent);
    }

    public async Task<StudentDTO> Delete(string studentId)
    {
        var objectId = new ObjectId(studentId);
        var existingStudent = await _students.Find(student => student.Id == objectId).FirstOrDefaultAsync();

        if (existingStudent == null)
        {
            return null;
        }
        
        await _students.DeleteOneAsync(item => item.Id == objectId);
        
        return _mapper.Map<StudentDTO>(existingStudent);
    }

    public async Task<List<StudentDTO>> GetAll()
    {
     var students = await _students.Find(listStudents => true).ToListAsync();
     
     return _mapper.Map<List<StudentDTO>>(students).ToList();
    }
    
    public async Task<StudentDTO> GetById(string studentId)
    {
        var objectId = new ObjectId(studentId);
        var student = await _students.Find(student => student.Id == objectId).FirstOrDefaultAsync();
        if (student == null)
        {
            return null;
        }
        
        return _mapper.Map<StudentDTO>(student);;
    }
}