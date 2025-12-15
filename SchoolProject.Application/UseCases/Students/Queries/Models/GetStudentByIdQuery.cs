using SchoolProject.Application.Bases;
using SchoolProject.Application.Bases.CQRS;
using SchoolProject.Application.UseCases.Students.Queries.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolProject.Application.UseCases.Students.Queries.Models;

public class GetStudentByIdQuery : IQuery<Response<SingleStudentResult>>
{
   public int Id { get; set; }

	public GetStudentByIdQuery(int id)
	{
		this.Id = id;
   }

}
