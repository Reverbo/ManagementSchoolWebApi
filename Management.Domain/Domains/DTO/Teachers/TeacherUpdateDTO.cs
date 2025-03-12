using AutoMapper;
using Management.Domain.Gateway.Teacher;

namespace Management.Domain.Domains.DTO.Teachers;

public class TeacherUpdateDTO 
{
    public required string FullName { get; set; }
    
    public required string TeacherContact { get; set; }
    
    public required string DisciplineId { get; set; }

    public required string ClassroomId { get; set; }

    public required decimal Salary { get; set; }
}