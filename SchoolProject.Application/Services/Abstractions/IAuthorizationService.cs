namespace SchoolProject.Application.Services.Abstractions;

public interface IAuthorizationService
{
   Task<string> AddRoleAsync(string roleName);
   Task<bool> IsRoleExistsAsync(string roleName);

}
