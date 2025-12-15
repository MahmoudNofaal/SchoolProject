namespace SchoolProject.Application.UseCases.Departments.Queries.Results;

public class SingleStudentResult
{
   public SingleStudentResult(int id, string name)
   {
      Id = id;
      Name = name;
   }

   public int Id { get; set; }
   public string Name { get; set; }
}
