namespace Management.Domain.Domains.DTO.Teachers;

public interface ITeacher
{
    public string FullName { get; set; }
    public string DateBirth { get; set; }
    public string Cpf { get; set; }
    public string TeacherContact { get; set; }
    public string DisciplineId { get; set; }
    public string ClassroomId { get; set; }
    public decimal Salary { get; set; }
}