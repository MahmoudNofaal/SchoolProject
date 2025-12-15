using Microsoft.AspNetCore.Identity;
using SchoolProject.Application.Services.Abstractions;
using SchoolProject.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Application.Services;

public class AuthorizationService : IAuthorizationService
{
   private readonly RoleManager<ApplicationRole> _roleManager;

   public AuthorizationService(RoleManager<ApplicationRole> roleManager)
   {
      this._roleManager = roleManager;
   }

   public async Task<string> AddRoleAsync(string roleName)
   {
      var applicationRole = new ApplicationRole()
      {
         Name = roleName
      };

      var result = await _roleManager.CreateAsync(applicationRole);

      if (result.Succeeded)
      {
         return "Success";
      }

      return "Failed";
   }

   public async Task<bool> IsRoleExistsAsync(string roleName)
   {
      return await _roleManager.RoleExistsAsync(roleName);
   }


}
