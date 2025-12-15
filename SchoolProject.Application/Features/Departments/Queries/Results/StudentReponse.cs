namespace SchoolProject.Application.Features.Departments.Queries.Results;

public class StudentReponse
{
   public StudentReponse(int id, string name)
   {
      Id = id;
      Name = name;
   }

   public int Id { get; set; }
   public string Name { get; set; }
}
