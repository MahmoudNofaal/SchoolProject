using SchoolProject.Application.UseCases.Authorization.Queries.Results;
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

}
