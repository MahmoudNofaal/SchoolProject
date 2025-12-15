using SchoolProject.Application.Bases;
using SchoolProject.Application.Bases.CQRS;
using SchoolProject.Application.UseCases.Authorization.Queries.Results;

namespace SchoolProject.Application.UseCases.Authorization.Queries.Models;

public class GetRoleByIdQuery : IQuery<Response<SingleRoleResult>>
{
   public int Id { get; set; }

   public GetRoleByIdQuery(int id)
   {
      Id = id;
   }
}
