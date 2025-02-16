using Management.Domain.Domains.DTO.Students;
using Management.Resource.Student;

namespace Management.Resource.Classroom;

public class ClassroomResponseResource
{
    public string? Id { get; set; }

    public required string ClassName { get; set; }

    public required string SchoolYear { get; set; }

    public required List<StudentResource> Students { get; set; }
}