using SchoolProject.Application.Bases.CQRS;
using SchoolProject.Application.Features.Students.Queries.Results;
using SchoolProject.Application.Wrappers;
using SchoolProject.Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolProject.Application.Features.Students.Queries.Models;

public class GetStudentPaginatedListQuery : IQuery<PaginatedResult<GetStudentPaginatedListResponse>>
{
   public int PageNumber { get; set; } = 1;
   public int PageSize { get; set; } = 10;
   public StudentOrderingEnum OrderBy { get; set; }
   public string? Search { get; set; }


}
