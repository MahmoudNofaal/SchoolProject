using SchoolProject.Application.Bases;
using SchoolProject.Application.Bases.CQRS;
using SchoolProject.Application.UseCases.Authorization.Queries.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Application.UseCases.Authorization.Queries.Models;

public class GetRolesListQuery : IQuery<Response<IEnumerable<RolesListResult>>>
{
}
