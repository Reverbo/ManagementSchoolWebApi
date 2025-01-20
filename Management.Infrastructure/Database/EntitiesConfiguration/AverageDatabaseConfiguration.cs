using Management.Infrastructure.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MongoDB.EntityFrameworkCore.Extensions;

namespace Management.Infrastructure.Database.EntitiesConfiguration;

public class AverageDatabaseConfiguration: IEntityTypeConfiguration<AverageEntity>
{
    public void Configure(EntityTypeBuilder<AverageEntity> builder)
    {
        builder.ToCollection("averages");
    }
}