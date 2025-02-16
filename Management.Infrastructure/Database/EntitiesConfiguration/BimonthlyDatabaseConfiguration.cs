using Management.Infrastructure.Database.Entities.Bimonthly;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MongoDB.EntityFrameworkCore.Extensions;

namespace Management.Infrastructure.Database.EntitiesConfiguration;

public class BimonthlyDatabaseConfiguration: IEntityTypeConfiguration<BimonthlyEntity>
{
    public void Configure(EntityTypeBuilder<BimonthlyEntity> builder)
    {
        builder.ToCollection("bimonthlys");
    }
}