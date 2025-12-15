using SchoolProject.Application.Bases;
using SchoolProject.Application.Bases.CQRS;

namespace SchoolProject.Application.Features.ApplicationUsers.Commands.Models;

public class UpdateApplicationUserCommand : ICommand<Response<string>>
{
   public int Id { get; set; }
   public string FullName { get; set; }
   public string Email { get; set; }
   public string? Address { get; set; }
   public string? Country { get; set; }
   public string? PhoneNumber { get; set; }

}

public class DeleteApplicationUserCommand : ICommand<Response<string>>
{
   public int Id { get; set; }
  
   public DeleteApplicationUserCommand(int id)
   {
      Id = id;
   }
}
