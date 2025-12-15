using SchoolProject.Application.Bases;
using SchoolProject.Application.Bases.CQRS;
using SchoolProject.Application.Features.ApplicationUsers.Queries.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolProject.Application.Features.ApplicationUsers.Queries.Models;

public class GetUserByIdQuery : IQuery<Response<GetSingleUserResponse>>
{
   public GetUserByIdQuery(int id)
   {
      Id = id;
   }

   public int Id { get; set; }

}
