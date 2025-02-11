using Management.Infrastructure.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MongoDB.EntityFrameworkCore.Extensions;

namespace Management.Infrastructure.Database.EntitiesConfiguration;

public class DisciplineDatabaseConfiguration : IEntityTypeConfiguration<DisciplineEntity>
{
    public void Configure(EntityTypeBuilder<DisciplineEntity> builder)
    {
        builder.ToCollection("disciplines");
    }
}