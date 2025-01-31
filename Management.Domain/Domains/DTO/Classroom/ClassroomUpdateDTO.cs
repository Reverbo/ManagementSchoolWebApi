using Management.Domain.Domains.DTO.Students;

namespace Management.Domain.Domains.DTO.Classroom;

public class ClassroomUpdateDTO
{
    public required List<string> StudentsId { get; set; }
}