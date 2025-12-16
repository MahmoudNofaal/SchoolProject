using SchoolProject.Application.UseCases.Authorization.Queries.Results;
using SchoolProject.Domain.DTOs;
using SchoolProject.Domain.Entities.Identity;

namespace SchoolProject.Application.Services.Abstractions;

public interface IAuthorizationService
{
   Task<string> AddRoleAsync(string roleName);
   Task<string> EditRoleAsync(int id, string roleName);
   Task<string> DeleteRoleAsync(int id);
   Task<bool> IsRoleExistsAsync(string roleName);

   Task<IEnumerable<ApplicationRole>> GetRolesListAsync();
   Task<ApplicationRole> GetRoleByIdAsync(int id);

   Task<UserRolesResult> GetRolesAndUserRolesAsync(ApplicationUser user);
   Task<UserClaimsResult> GetClaimsAndUserClaimsAsync(ApplicationUser user);

   Task<string> UpdateUserRolesAsync(ApplicationUser user, List<RoleDTO> roles);
   Task<string> UpdateUserClaimsAsync(ApplicationUser user, List<ClaimDTO> claims);

}
