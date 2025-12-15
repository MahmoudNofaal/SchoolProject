using SchoolProject.Application.Wrappers;

namespace SchoolProject.Application.Features.Departments.Queries.Results;

public class GetSingleDepartmentResponse
{

   public int Id { get; set; }

   public string? Name { get; set; }

   public string? ManagerName { get; set; }

   public PaginatedResult<StudentReponse>? StudentsList { get; set; }
   public IEnumerable<SubjectReponse>? Subjects { get; set; }
   public IEnumerable<InstructorReponse>? Instructors { get; set; }

}
