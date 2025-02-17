namespace Management.Resource.Student;

public class StudentUpdateResource
{
    public required string FullName { get; set; }
    public required string SocialName { get; set; }
    public required string FatherName { get; set; }
    public required string MotherName { get; set; }
    public required string ClassroomId { get; set; }
}