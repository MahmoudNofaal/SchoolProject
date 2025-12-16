using SchoolProject.Application.Bases;
using SchoolProject.Application.Bases.CQRS;
using SchoolProject.Application.UseCases.Authorization.Queries.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Application.UseCases.Authorization.Queries.Models;

public class GetUserClaimsQuery : IQuery<Response<UserClaimsResult>>
{
   public int UserId { get; set; }

   public GetUserClaimsQuery(int userId)
   {
      UserId = userId;
   }
}
