using Management.Domain.Services;
using Management.Infrasctructure.Database.Entities;
using Management.Infrasctructure.Database.EntitiesConfiguration;
using MongoDB.Driver;


var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();
builder.Services.Configure<StudentDatabaseConfiguration>(
    builder.Configuration.GetSection("MongoDB")
);
//var mongoClient = new MongoClient(builder.Configuration["MongoDB:ConnectionString"]);
//var database = mongoClient.GetDatabase("ManagementSchools");
builder.Services.AddSingleton<StudentsServices>();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers(); // Mapeia os controladores automaticamente


app.Run();
