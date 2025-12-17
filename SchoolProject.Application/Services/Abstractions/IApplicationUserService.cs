using SchoolProject.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Application.Services.Abstractions;

public interface IApplicationUserService
{

   Task<string> CreateUserAsync(ApplicationUser user, string password);

}
