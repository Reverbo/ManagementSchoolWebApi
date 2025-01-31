namespace Management.Domain.Domains.DTO.Teachers;

public class TeacherDTO
{
    public string Id { get; set; }

    public required string FullName { get; set; }

    public required int Age { get; set; }

    public required string ClassroomDiscipline { get; set; }

    public required string Contact { get; set; }

    public required string ClassTeaching { get; set; }

    public required decimal Salary { get; set; }
}