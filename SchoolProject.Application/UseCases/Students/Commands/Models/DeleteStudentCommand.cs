using SchoolProject.Application.Bases;
using SchoolProject.Application.Bases.CQRS;

namespace SchoolProject.Application.UseCases.Students.Commands.Models;

public class DeleteStudentCommand : ICommand<Response<string>>
{
   public int Id { get; set; }

	public DeleteStudentCommand(int id)
	{
		this.Id = id;
   }

}
