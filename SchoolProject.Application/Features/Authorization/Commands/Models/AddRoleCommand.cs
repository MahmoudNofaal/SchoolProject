using SchoolProject.Application.Bases;
using SchoolProject.Application.Bases.CQRS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Application.Features.Authorization.Commands.Models;

public class AddRoleCommand : ICommand<Response<string>>
{
   public string RoleName { get; set; }

}
