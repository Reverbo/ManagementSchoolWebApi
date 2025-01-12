using Management.Infrasctructure.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Management.Infrasctructure.Database.Persistence;

public class ManagementSchoolDbContext : DbContext
{
    public ManagementSchoolDbContext(DbContextOptions<ManagementSchoolDbContext> options) : base(options)
    {
    }

    public DbSet<StudentEntity> StudentEntities { get; set; }
    public DbSet<TeacherEntity> TeacherEntities { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ManagementSchoolDbContext).Assembly);
    }
}