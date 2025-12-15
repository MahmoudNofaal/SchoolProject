namespace SchoolProject.Application.UseCases.Students.Queries.Results;

public class SingleStudentResult
{
   public int Id { get; set; }

   public string? Name { get; set; }
   public string? Address { get; set; }
   public string? DepartmentName { get; set; }

}