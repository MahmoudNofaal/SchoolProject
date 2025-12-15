using SchoolProject.Application.Bases;
using SchoolProject.Application.Bases.CQRS;
using SchoolProject.Application.UseCases.Departments.Queries.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolProject.Application.UseCases.Departments.Queries.Models;

public class GetDepartmentByIdQuery : IQuery<Response<SingleDepartmentResult>>
{
   public int Id { get; set; }
	public int StudentPageNumber { get; set; }
	public int StudentPageSize { get; set; }

}
