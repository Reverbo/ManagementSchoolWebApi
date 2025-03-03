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
using Management.Filters.InputFilter.Interfaces;
using Management.Filters.InputFilter.Validators.Average;
using Management.Filters.InputFilter.Validators.Bimonthly;
using Management.Filters.InputFilter.Validators.Classroom;
using Management.Filters.InputFilter.Validators.Discipline;
using Management.Filters.InputFilter.Validators.Student;
using Management.Filters.InputFilter.Validators.Teacher;
using Management.Infrastructure.Database.Repositories;
using Management.Mappings;
using Management.Resource.Average;
using Management.Resource.Bimonthly;
using Management.Resource.Classroom;
using Management.Resource.Discipline;
using Management.Resource.Student;
using Management.Resource.Teachers;
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

        services.AddScoped<IValidatorBase<BimonthlyCreateResource>, BimonthlyValidator>();
        services.AddScoped<IValidatorBase<BimonthlyDatesResource>, BimonthlyUpdateValidator>();
        services.AddScoped<IValidatorBase<BimonthlyUpdateDisciplinesResource>, BimonthlyUpdateDisciplinesValidator>();

        services.AddScoped<IValidatorBase<AverageCreateResource>, AverageValidator>();
        services.AddScoped<IValidatorBase<ScoresResource>, ScoreValidator>();
        
        services.AddScoped<IValidatorBase<TeacherResource>, TeacherValidator>();
        services.AddScoped<IValidatorBase<TeacherUpdateResource>, TeacherUpdateValidator>();

        services.AddScoped<IValidatorBase<StudentResource>, StudentValidator>();
        services.AddScoped<IValidatorBase<StudentUpdateResource>, StudentUpdateValidator>();
        
        services.AddScoped<IValidatorBase<DisciplineCreateResource>, DisciplineValidator>();
        services.AddScoped<IValidatorBase<DisciplineUpdateResource>, DisciplineUpdateValidator>();
        services.AddScoped<IValidatorBase<DisciplineUpdateAveragesResource>, DisciplineUpdateAverageValidator>();

        services.AddScoped<IValidatorBase<ClassroomResource>, ClassroomValidator>();
        services.AddScoped<IValidatorBase<ClassroomUpdateResource>, ClassroomUpdateValidator>();
        services.AddScoped<IValidatorBase<ClassroomUpdateStudentsResource>, ClassroomUpdateStudentsValidator>();



        return services;
    }
}