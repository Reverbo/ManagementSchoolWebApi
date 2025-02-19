namespace Management.Resource.Teachers;

public class TeacherUpdateResource
{
   public required string FullName { get; set; }
    
   public required string TeacherContact { get; set; }
    
   public required string DisciplineId { get; set; }

   public required string ClassroomId { get; set; }

   public required decimal Salary { get; set; }
    
}