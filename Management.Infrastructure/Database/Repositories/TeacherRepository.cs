using AutoMapper;
using Management.Domain.Domains.DTO.Teachers;
using Management.Domain.Gateway.Teacher;
using Management.Infrastructure.Database.Entities.Teacher;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Management.Infrastructure.Database.Repositories;

public class TeacherRepository : ITeacherRepositoryGateway
{
    private readonly IMongoCollection<TeacherEntity> _teachers;
    private readonly IMapper _mapper;

    public TeacherRepository(IMongoDatabase database, IMapper mapper)
    {
        _teachers = database.GetCollection<TeacherEntity>("teachers");
        _mapper = mapper;
    }

    public async Task<TeacherDTO> Create(TeacherDTO teacher)
    {
        var teacherEntity = _mapper.Map<TeacherEntity>(teacher);
        teacherEntity.Id = ObjectId.GenerateNewId();
        await _teachers.InsertOneAsync(teacherEntity);
        return _mapper.Map<TeacherDTO>(teacherEntity);
    }

    public async Task<TeacherDTO?> Update(TeacherUpdateDTO teacher, string teacherId)
    {
        var teacherObjectId = new ObjectId(teacherId);
        var teacherEntity =
            _teachers.FindAsync(item => item.Id == teacherObjectId)
                .Result.FirstOrDefault();

        if (teacherEntity == null)
        {
            return null;
        }
        
        teacherEntity.UpdateByTeacherUpdateDto(teacher);
        
        var result = await _teachers.ReplaceOneAsync(item => item.Id == teacherObjectId, teacherEntity);
        
        if (!result.IsAcknowledged)
        {
            return null;
        }

        var updatedEntity = await _teachers.Find(item => item.Id == teacherObjectId).FirstOrDefaultAsync();

        return _mapper.Map<TeacherDTO>(updatedEntity);
    }

    public async Task<TeacherDTO?> Delete(string teacherId)
    {
        var teacherObjectId = new ObjectId(teacherId);
        var entityToDelete =
            _teachers.FindAsync(item => item.Id == teacherObjectId)
                .Result
                .FirstOrDefault();

        if (entityToDelete == null)
        {
            return null;
        }

        await _teachers.DeleteOneAsync(item => item.Id == teacherObjectId);

        return _mapper.Map<TeacherDTO>(entityToDelete);
    }

    public async Task<List<TeacherDTO>> GetAll()
    {
        var teacherList = await _teachers.Find(_ => true).ToListAsync();

        return _mapper.Map<List<TeacherDTO>>(teacherList).ToList();
    }

    public async Task<TeacherDTO?> GetById(string teacherId)
    {
        var teacherObjectId = new ObjectId(teacherId);
        var teacher = await _teachers.Find(item => item.Id == teacherObjectId).FirstOrDefaultAsync();

        if (teacher == null)
        {
            return null;
        }

        return _mapper.Map<TeacherDTO>(teacher);
    }
    
    public async Task<TeacherDTO?> GetByCpf(string cpf)
    {
        var existingStudent = await _teachers.Find(student => student.Cpf.Equals(cpf)).FirstOrDefaultAsync();
        if (existingStudent == null)
        {
            return null;
        }

        return _mapper.Map<TeacherDTO>(existingStudent);
    }
}