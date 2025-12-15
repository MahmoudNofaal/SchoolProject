using SchoolProject.Application.Bases;
using SchoolProject.Application.Bases.CQRS;

namespace SchoolProject.Application.UseCases.Authorization.Commands.Models;

public class EditRoleCommand : ICommand<Response<string>>
{
   public int Id { get; set; }
   public string RoleName { get; set; }

}

