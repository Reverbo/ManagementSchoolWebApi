using Management.Infrastructure.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MongoDB.EntityFrameworkCore.Extensions;

namespace Management.Infrastructure.Database.EntitiesConfiguration;

public class StudentDatabaseConfiguration : IEntityTypeConfiguration<StudentEntity>
{
    public void Configure(EntityTypeBuilder<StudentEntity> builder)
    {
        builder.ToCollection("students");
    }
}