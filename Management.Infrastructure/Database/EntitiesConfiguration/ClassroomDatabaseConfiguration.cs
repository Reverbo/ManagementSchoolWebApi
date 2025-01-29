using Management.Infrastructure.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MongoDB.EntityFrameworkCore.Extensions;

namespace Management.Infrastructure.Database.EntitiesConfiguration;

public class ClassroomDatabaseConfiguration : IEntityTypeConfiguration<ClassroomEntity>
{
    public void Configure(EntityTypeBuilder<ClassroomEntity> builder)
    {
        builder.ToCollection("classrooms");
    }
}