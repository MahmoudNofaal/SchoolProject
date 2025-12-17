using SchoolProject.Application.Bases;
using SchoolProject.Application.Bases.CQRS;

namespace SchoolProject.Application.UseCases.Authentication.Queries.Models;

public class AuthorizeUserQuery : IQuery<Response<string>>
{
   public string AccessToken { get; set; }

}
