using Management.Domain.Gateway.Student;
using Management.Domain.Services;
using Management.Domain.UseCases.Students;
using Management.Infrasctructure.Database.Entities;
using Management.Infrastructure.Database.Repositories;
using Management.Mappings;
using MongoDB.Driver;

namespace Management.DependecyInjection;

public static class DependecyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var mongoConnectionString = configuration.GetConnectionString("MongoDBConnection");
        var databaseName = configuration.GetConnectionString("DatabaseName");

        if (string.IsNullOrEmpty(mongoConnectionString) || string.IsNullOrEmpty(databaseName))
        {
            throw new ArgumentException("MongoDb connectionString or databaseName not configured.");
        }

        var mongoClient = new MongoClient(mongoConnectionString);
        var database = mongoClient.GetDatabase(databaseName);

        services.AddSingleton<IMongoClient>(mongoClient);
        services.AddScoped<IMongoDatabase>(_ => database);

        services.AddControllers();

        services.AddAutoMapper(typeof(ResourceToDtoProfileStudents));
        
        services.AddScoped<IStudentCrudUseCase, StudentCrudService>();
        services.AddScoped<IStudentReposityGateway, StudentRepository>();

        return services;
    }
}