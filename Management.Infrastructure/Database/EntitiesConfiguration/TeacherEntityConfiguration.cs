using Management.Infrasctructure.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MongoDB.EntityFrameworkCore.Extensions;

namespace Management.Infrasctructure.Database.EntitiesConfiguration;

public class TeacherEntityConfiguration: IEntityTypeConfiguration<TeacherEntity>
{
    public void Configure(EntityTypeBuilder<TeacherEntity> builder)
    {
        builder.ToCollection("teachers");
    }
}