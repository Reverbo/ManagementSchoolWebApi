using Management.Domain.Gateway;
using Management.Domain.Gateway.Average;
using Management.Domain.Gateway.Bimonthly;
using Management.Domain.Gateway.Classroom;
using Management.Domain.Gateway.Student;
using Management.Domain.Gateway.Teacher;
using Management.Domain.Services;
using Management.Domain.UseCases.Average;
using Management.Domain.UseCases.Bimonthly;
using Management.Domain.UseCases.Classroom;
using Management.Domain.UseCases.Discipline;
using Management.Domain.UseCases.Students;
using Management.Domain.UseCases.Teachers;
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

        services.AddAutoMapper(
            typeof(ResourceToDtoProfileStudent),
            typeof(ResourceToDtoProfileTeacher),
            typeof(ResourceToDtoProfileClassroom),
            typeof(ResourceToDtoProfileAverage),
            typeof(ResourceToDtoProfileDiscipline),
            typeof(ResourceToDtoProfileBimonthly)
        );

        services.AddScoped<IStudentCrudUseCase, StudentCrudService>();
        services.AddScoped<IStudentReposityGateway, StudentRepository>();

        services.AddScoped<IClassroomCrudUseCases, ClassroomCrudService>();
        services.AddScoped<IClassroomRepositoryGateway, ClassroomRepository>();

        services.AddScoped<ITeacherCrudUseCase, TeacherCrudService>();
        services.AddScoped<ITeacherRepositoryGateway, TeacherRepository>();

        services.AddScoped<IAverageCrudUseCase, AverageCrudService>();
        services.AddScoped<IAverageRepositoryGateway, AverageRepository>();
        
        services.AddScoped<IDisciplineCrudUseCase, DisciplineCrudService>();
        services.AddScoped<IDisciplineRepositoryGateway, DisciplineRepository>();

        services.AddScoped<IBimonthlyCrudUseCase, BimonthlyCrudService>();
        services.AddScoped<IBimonthlyRepositoryGateway, BimonthlyRepository>();

        return services;
    }
}