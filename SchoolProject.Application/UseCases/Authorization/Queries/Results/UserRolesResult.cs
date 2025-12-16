using SchoolProject.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Application.UseCases.Authorization.Queries.Results;

public class UserRolesResult
{
   public int UserId { get; set; }
   public ICollection<RoleDTO> Roles { get; set; }

}


