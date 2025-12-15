using SchoolProject.Application.Bases;
using SchoolProject.Application.Bases.CQRS;

namespace SchoolProject.Application.UseCases.Authorization.Commands.Models;

public class DeleteRoleCommand : ICommand<Response<string>>
{
   public int Id { get; set; }

   public DeleteRoleCommand(int id)
   {
      Id = id;
   }
}

