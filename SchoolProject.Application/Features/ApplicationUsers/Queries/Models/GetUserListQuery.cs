using SchoolProject.Application.Bases.CQRS;
using SchoolProject.Application.Features.ApplicationUsers.Queries.Results;
using SchoolProject.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolProject.Application.Features.ApplicationUsers.Queries.Models;

public class GetUserListQuery : IQuery<PaginatedResult<GetUserListResponse>>
{
   public int PageNumber { get; set; }
   public int PageSize { get; set; }

}
