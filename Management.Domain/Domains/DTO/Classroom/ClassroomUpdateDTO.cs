using Management.Domain.Domains.DTO.Students;

namespace Management.Domain.Domains.DTO.Classroom;

public class ClassroomUpdateDTO
{
    public required string ClassName { get; set; }

    public required string SchoolYear { get; set; }

    public required string BimonthlyId { get; set; }
}