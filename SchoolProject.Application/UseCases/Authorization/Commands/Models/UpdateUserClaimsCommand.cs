using SchoolProject.Application.Bases;
using SchoolProject.Application.Bases.CQRS;
using SchoolProject.Domain.DTOs;

namespace SchoolProject.Application.UseCases.Authorization.Commands.Models;

public class UpdateUserClaimsCommand : ICommand<Response<string>>
{
   public int UserId { get; set; }
   public ICollection<ClaimDTO> Claims { get; set; }

}

