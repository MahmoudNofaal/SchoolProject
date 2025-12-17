using SchoolProject.Application.Bases;
using SchoolProject.Application.Bases.CQRS;

namespace SchoolProject.Application.UseCases.Authentication.Commands.Models;

public class ConfirmUserEmailCommand : ICommand<Response<string>>
{
   public int UserId { get; set; }
   public string Code { get; set; }

}
