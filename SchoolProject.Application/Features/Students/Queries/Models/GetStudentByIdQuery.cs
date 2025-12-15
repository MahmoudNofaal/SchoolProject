using SchoolProject.Application.Bases;
using SchoolProject.Application.Bases.CQRS;
using SchoolProject.Application.Features.Students.Queries.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolProject.Application.Features.Students.Queries.Models;

public class GetStudentByIdQuery : IQuery<Response<GetSingleStudentReponse>>
{
   public int Id { get; set; }

	public GetStudentByIdQuery(int id)
	{
		this.Id = id;
   }

}
