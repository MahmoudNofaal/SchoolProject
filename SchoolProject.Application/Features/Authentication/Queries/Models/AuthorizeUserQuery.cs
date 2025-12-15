using SchoolProject.Application.Bases;
using SchoolProject.Application.Bases.CQRS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Application.Features.Authentication.Queries.Models;

public class AuthorizeUserQuery : IQuery<Response<string>>
{
   public string AccessToken { get; set; }

}
