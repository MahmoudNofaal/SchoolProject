namespace SchoolProject.Application.UseCases.Students.Queries.Results;

public class StudentPaginatedListResult
{
   public int Id { get; set; }

   public string? Name { get; set; }
   public string? Address { get; set; }
   public string? DepartmentName { get; set; }

   public StudentPaginatedListResult()
   {
   }

   public StudentPaginatedListResult(int studID, string? name, string? address, string? departmentName)
   {
      Id = studID;  
      Name = name;
      Address = address;
      DepartmentName = departmentName;
   }

}
