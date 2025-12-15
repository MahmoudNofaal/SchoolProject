using SchoolProject.Application.Bases;
using SchoolProject.Application.Bases.CQRS;
using SchoolProject.Application.UseCases.Students.Queries.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolProject.Application.UseCases.Students.Queries.Models;

public class GetStudentListQuery : IQuery<Response<IEnumerable<StudentListResult>>>
{


}
