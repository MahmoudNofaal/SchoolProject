using SchoolProject.Application.Bases.CQRS;
using SchoolProject.Application.UseCases.Students.Queries.Results;
using SchoolProject.Application.Wrappers;
using SchoolProject.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolProject.Application.UseCases.Students.Queries.Models;

public class GetStudentPaginatedListQuery : IQuery<PaginatedResult<StudentPaginatedListResult>>
{
   public int PageNumber { get; set; } = 1;
   public int PageSize { get; set; } = 10;
   public StudentOrderingEnum OrderBy { get; set; }
   public string? Search { get; set; }


}
