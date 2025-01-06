namespace Management.Infrasctructure.Database.EntitiesConfiguration;

public class StudentDatabaseConfiguration
{
    public string ConnectionString { get; set; } = null;
    public string DatabaseName { get; set; }= null;
    public string StudentCollectionName { get; set; }= null;
}