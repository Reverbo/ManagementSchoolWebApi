using AutoMapper;
using Management.Domain.Domains.DTO.Teachers;
using Management.Domain.Gateway.Teacher;
using Management.Infrasctructure.Database.Entities;
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

    public async Task<TeacherDTO?> Update(TeacherDTO teacher, string teacherId)
    {
        var teacherObjectIdId = new ObjectId(teacherId);

        var existingEntity =
            _teachers.FindAsync(item => item.Id == teacherObjectIdId)
                .Result.FirstOrDefault();

        if (existingEntity == null)
        {
            return null;
        }

        existingEntity.Age = teacher.Age;
        existingEntity.Contact = teacher.Contact;
        existingEntity.ClassTeaching = teacher.ClassTeaching;
        existingEntity.Salary = teacher.Salary;
        existingEntity.ClassroomDiscipline = teacher.ClassroomDiscipline;
        existingEntity.FullName = teacher.FullName;

        var result = await _teachers.ReplaceOneAsync(item => item.Id == teacherObjectIdId, existingEntity);
        
        if (!result.IsAcknowledged)
        {
            return null;
        }

        var updatedEntity = await _teachers.Find(item => item.Id == teacherObjectIdId).FirstOrDefaultAsync();

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
        var entityFind = await _teachers.Find(item => item.Id == teacherObjectId).FirstOrDefaultAsync();

        if (entityFind == null)
        {
            return null;
        }

        return _mapper.Map<TeacherDTO>(entityFind);
    }
}