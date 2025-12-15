using SchoolProject.Application.Bases;
using SchoolProject.Application.Bases.CQRS;
using SchoolProject.Application.UseCases.ApplicationUsers.Queries.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolProject.Application.UseCases.ApplicationUsers.Queries.Models;

public class GetUserByIdQuery : IQuery<Response<SingleUserResult>>
{
   public GetUserByIdQuery(int id)
   {
      Id = id;
   }

   public int Id { get; set; }

}
