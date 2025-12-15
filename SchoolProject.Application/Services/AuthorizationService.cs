using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SchoolProject.Application.Services.Abstractions;
using SchoolProject.Domain.Entities.Identity;

namespace SchoolProject.Application.Services;

public class AuthorizationService : IAuthorizationService
{
   private readonly RoleManager<ApplicationRole> _roleManager;
   private readonly UserManager<ApplicationUser> _userManager;

   public AuthorizationService(RoleManager<ApplicationRole> roleManager,
                               UserManager<ApplicationUser> userManager)
   {
      this._roleManager = roleManager;
      this._userManager = userManager;
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

   public async Task<string> DeleteRoleAsync(int id)
   {
      var roleToDelete = await _roleManager.FindByIdAsync(id.ToString());

      if (roleToDelete is null)
      {
         return "Failed";
      }

      var users = await _userManager.GetUsersInRoleAsync(roleToDelete.Name);

      if (users != null)
      {
         return "Used";
      }

      var result = await _roleManager.DeleteAsync(roleToDelete);

      if (result.Succeeded)
      {
         return "Success";
      }

      return string.Join("-", result.Errors);
   }

   public async Task<string> EditRoleAsync(int id, string roleName)
   {
      var roleToUpdate = await _roleManager.FindByIdAsync(id.ToString());

      if (roleToUpdate is null)
      {
         return "Failed";
      }

      roleToUpdate.Name = roleName;

      var result = await _roleManager.UpdateAsync(roleToUpdate);

      if (result.Succeeded)
      {
         return "Success";
      }

      return string.Join("-", result.Errors);
   }

   public async Task<ApplicationRole> GetRoleByIdAsync(int id)
   {
      return await _roleManager.FindByIdAsync(id.ToString());
   }

   public async Task<IEnumerable<ApplicationRole>> GetRolesListAsync()
   {
      return await _roleManager.Roles.ToListAsync();
   }

   public async Task<bool> IsRoleExistsAsync(string roleName)
   {
      return await _roleManager.RoleExistsAsync(roleName);
   }


}
