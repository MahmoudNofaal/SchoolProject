using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using SchoolProject.Application.Bases;
using SchoolProject.Application.Bases.CQRS;
using SchoolProject.Application.Resources;
using SchoolProject.Application.Services.Abstractions;
using SchoolProject.Application.UseCases.Authorization.Commands.Models;
using SchoolProject.Domain.Entities.Identity;

namespace SchoolProject.Application.UseCases.Authorization.Commands.Handlers;

public class RoleCommandHandler : ResponseHandler,
                                           ICommandHandler<AddRoleCommand, Response<string>>,
                                           ICommandHandler<EditRoleCommand, Response<string>>,
                                           ICommandHandler<DeleteRoleCommand, Response<string>>,
                                           ICommandHandler<UpdateUserRolesCommand, Response<string>>
{
   private readonly IAuthorizationService _authorizationService;
   private readonly UserManager<ApplicationUser> _userManager;

   public RoleCommandHandler(IStringLocalizer<SharedResources> stringLocalizer,
                                      IAuthorizationService authorizationService,
                                      UserManager<ApplicationUser> userManager) : base(stringLocalizer)
   {
      this._authorizationService = authorizationService;
      this._userManager = userManager;
   }

   #region Add Role - Handle
   public async Task<Response<string>> Handle(AddRoleCommand request, CancellationToken cancellationToken)
   {
      var result = await _authorizationService.AddRoleAsync(request.RoleName);

      if (result == "Success")
      {
         return Success("Role added successfully");
      }

      return BadRequest<string>();
   }
   #endregion

   #region Edit Role - Handle
   public async Task<Response<string>> Handle(EditRoleCommand request, CancellationToken cancellationToken)
   {
      var result = await _authorizationService.EditRoleAsync(request.Id, request.RoleName);

      if (result == "Success")
      {
         return Success("Role updated successfully");
      }

      return BadRequest<string>(result);
   }
   #endregion

   #region Delete Role - Handle
   public async Task<Response<string>> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
   {
      var result = await _authorizationService.DeleteRoleAsync(request.Id);

      if (result == "Success")
      {
         return Success("Role deleted successfully");
      }

      return BadRequest<string>(result);
   }
   #endregion

   #region Update User Roles - Handle
   public async Task<Response<string>> Handle(UpdateUserRolesCommand request, CancellationToken cancellationToken)
   {
      var user = await _userManager.FindByIdAsync(request.UserId.ToString());

      if (user == null)
      {
         return NotFound<string>();
      }

      var result = await _authorizationService.UpdateUserRolesAsync(user, request.Roles.ToList());

      if (result == "Success")
      {
         return Success("User roles updated successfully.");
      }

      return BadRequest<string>(result);
   }
   #endregion

}
