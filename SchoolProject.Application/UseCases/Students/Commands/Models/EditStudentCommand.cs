using SchoolProject.Application.Bases;
using SchoolProject.Application.Bases.CQRS;

namespace SchoolProject.Application.UseCases.Students.Commands.Models;

public class EditStudentCommand : ICommand<Response<string>>
{
   public int Id { get; set; }
   public string Name { get; set; }
   public string Address { get; set; }

   public string? Phone { get; set; }

   public int DepartmentId { get; set; }

}
