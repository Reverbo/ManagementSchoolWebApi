using Management.Infrasctructure.Database.Entities;
using Management.Infrasctructure.Database.EntitiesConfiguration;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Management.Domain.Services;

public class StudentsServices
{
    private readonly IMongoCollection<Student> _studentCollection;

    public StudentsServices(IOptions<StudentDatabaseConfiguration> studentConfiguration)
    {
        var mongoClient = new MongoClient(studentConfiguration.Value.ConnectionString);
     
        var database = mongoClient.GetDatabase(studentConfiguration.Value.DatabaseName);

        _studentCollection = database.GetCollection<Student>(studentConfiguration.Value.StudentCollectionName);

    }
 
 
    public async Task<List<Student>> GetAsync() => await _studentCollection.Find(_ => true).ToListAsync();

    public async Task CreaterAsync(Student newStudent) => await _studentCollection.InsertOneAsync(newStudent);

    public async Task ReplaceAsync(string id, Student newStudent) =>
        await _studentCollection.ReplaceOneAsync(x => x.Id == id, newStudent);

    public async Task RemoveAsync(string id) => await _studentCollection.DeleteOneAsync(x => x.Id == id);}