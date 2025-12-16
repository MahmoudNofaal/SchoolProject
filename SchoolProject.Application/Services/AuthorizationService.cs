using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SchoolProject.Application.Services.Abstractions;
using SchoolProject.Application.UseCases.Authorization.Queries.Results;
using SchoolProject.Domain.DTOs;
using SchoolProject.Domain.Entities.Identity;
using SchoolProject.Domain.Helpers;
using SchoolProject.Infrastructure.Context;
using System.Security.Claims;

namespace SchoolProject.Application.Services;

public class AuthorizationService : IAuthorizationService
{
   private readonly RoleManager<ApplicationRole> _roleManager;
   private readonly UserManager<ApplicationUser> _userManager;
   private readonly ApplicationDbContext _dbContext;

   public AuthorizationService(RoleManager<ApplicationRole> roleManager,
                               UserManager<ApplicationUser> userManager,
                               ApplicationDbContext dbContext)
   {
      this._roleManager = roleManager;
      this._userManager = userManager;
      this._dbContext = dbContext;
   }

   #region Add Role
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
   #endregion

   #region Delete Role
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
   #endregion

   #region Edit Role
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
   #endregion

   #region Get Role By Id
   public async Task<ApplicationRole> GetRoleByIdAsync(int id)
   {
      return await _roleManager.FindByIdAsync(id.ToString());
   }
   #endregion

   #region Get Roles And User Roles
   public async Task<UserRolesResult> GetRolesAndUserRolesAsync(ApplicationUser user)
   {
      // get userClaims 
      var userRoles = await _userManager.GetRolesAsync(user);

      // get system roles
      var roles = await _roleManager.Roles.ToListAsync();

      // define response
      var manageUserRoles = new UserRolesResult();
      var rolesResponse = new List<RoleDTO>();

      // loop in system roles
      foreach (var role in roles)
      {
         // initialize claim result
         var roleResult = new RoleDTO()
         {
            Id = role.Id,
            Name = role.Name
         };

         // check if the userClaims contains claim to true HasRole
         if (userRoles.Contains(role.Name))
         {
            roleResult.HasRole = true;
         }
         else
         {
            roleResult.HasRole = false;
         }

         // add system claim to response list
         rolesResponse.Add(roleResult);
      }

      // assign userId to response
      manageUserRoles.UserId = user.Id;
      manageUserRoles.Roles = rolesResponse;

      // return response
      return manageUserRoles;
   }
   #endregion

   #region Get Roles List
   public async Task<IEnumerable<ApplicationRole>> GetRolesListAsync()
   {
      return await _roleManager.Roles.ToListAsync();
   }
   #endregion

   #region Is Role Exists
   public async Task<bool> IsRoleExistsAsync(string roleName)
   {
      return await _roleManager.RoleExistsAsync(roleName);
   }
   #endregion

   #region Update User Roles
   public async Task<string> UpdateUserRolesAsync(ApplicationUser user, List<RoleDTO> roles)
   {
      // Begin Transaction
      var transaction = await _dbContext.Database.BeginTransactionAsync();

      try
      {

         // get user old roles
         var userOldRoles = await _userManager.GetRolesAsync(user);

         // delete old roles
         var deleteOldRolesResult = await _userManager.RemoveFromRolesAsync(user, userOldRoles);

         if (!deleteOldRolesResult.Succeeded)
         {
            return "Failed";
         }

         // check roles first
         foreach (var systemRole in roles)
         {
            var isSystemRoleExists = await _roleManager.RoleExistsAsync(systemRole.Name);

            if (!isSystemRoleExists)
            {
               return "Failed";
            }
         }

         // add new roles that HasRole==True
         var newRolesToAdd = roles.Where(x => x.HasRole == true).Select(x => x.Name);

         var addUserNewRolesResult = await _userManager.AddToRolesAsync(user, newRolesToAdd);

         if (!addUserNewRolesResult.Succeeded)
         {
            return "Failed";
         }

         // save changes
         await transaction.CommitAsync();

         // return result
         return "Success";
      }
      catch (Exception ex)
      {
         await transaction.RollbackAsync();

         return ex.Message;
      }
   }
   #endregion

   #region Get Claims and User Claims
   public async Task<UserClaimsResult> GetClaimsAndUserClaimsAsync(ApplicationUser user)
   {
      // get userClaims 
      var userClaims = await _userManager.GetClaimsAsync(user);

      // get system claims
      var claims = ClaimsStore.Cliams;

      // define response
      var manageUserClaims = new UserClaimsResult();
      var claimsResponse = new List<ClaimDTO>();

      // loop in system roles
      foreach (var claim in claims)
      {
         // initialize claim result
         var claimResult = new ClaimDTO()
         {
            Type = claim.Type,
            Value = Convert.ToBoolean(claim.Value)
         };

         // check if the userClaims contains claim to true HasRole
         if (userClaims.Any(x => x.Type == claim.Type))
         {
            claimResult.Value = true;
         }
         else
         {
            claimResult.Value = false;
         }

         // add system claim to response list
         claimsResponse.Add(claimResult);
      }

      // assign userId to response
      manageUserClaims.UserId = user.Id;
      manageUserClaims.Claims = claimsResponse;

      // return response
      return manageUserClaims;
   }
   #endregion

   #region Update User Claims
   public async Task<string> UpdateUserClaimsAsync(ApplicationUser user, List<ClaimDTO> claims)
   {
      // Begin Transaction
      var transaction = await _dbContext.Database.BeginTransactionAsync();

      try
      {
         // get user old roles
         var userOldClaims = await _userManager.GetClaimsAsync(user);

         // delete old roles
         var deleteOldClaimsResult = await _userManager.RemoveClaimsAsync(user, userOldClaims);

         if (!deleteOldClaimsResult.Succeeded)
         {
            return "Failed";
         }

         // check roles first
         foreach (var systemClaim in claims)
         {
            if (!ClaimsStore.Cliams.Any(x => x.Type == systemClaim.Type))
            {
               return "Failed";
            }
         }

         // add new roles that HasRole==True
         var newClaimsToAdd = claims.Where(x => x.Value == true).Select(x => new Claim(x.Type, x.Value.ToString()));

         var addUserNewClaimsResult = await _userManager.AddClaimsAsync(user, newClaimsToAdd);

         if (!addUserNewClaimsResult.Succeeded)
         {
            return "Failed";
         }

         // save changes
         await transaction.CommitAsync();

         // return result
         return "Success";
      }
      catch (Exception ex)
      {
         await transaction.RollbackAsync();

         return ex.Message;
      }
   }
   #endregion


}
