using SchoolProject.Application.Wrappers;

namespace SchoolProject.Application.UseCases.Departments.Queries.Results;

public class SingleDepartmentResult
{

   public int Id { get; set; }

   public string? Name { get; set; }

   public string? ManagerName { get; set; }

   public PaginatedResult<SingleStudentResult>? StudentsList { get; set; }
   public IEnumerable<SingleSubjectResult>? Subjects { get; set; }
   public IEnumerable<SingleInstructorResult>? Instructors { get; set; }

}
