using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using SchoolProject.Application.Bases;
using SchoolProject.Application.Bases.CQRS;
using SchoolProject.Application.Resources;
using SchoolProject.Application.Services.Abstractions;
using SchoolProject.Application.UseCases.Authorization.Commands.Models;
using SchoolProject.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Application.UseCases.Authorization.Commands.Handlers;

public class AuthorizationCommandHandler : ResponseHandler,
                                           ICommandHandler<AddRoleCommand, Response<string>>,
                                           ICommandHandler<EditRoleCommand, Response<string>>,
                                           ICommandHandler<DeleteRoleCommand, Response<string>>
{
   private readonly IAuthorizationService _authorizationService;

   public AuthorizationCommandHandler(IStringLocalizer<SharedResources> stringLocalizer,
                                      IAuthorizationService authorizationService) : base(stringLocalizer)
   {
      this._authorizationService = authorizationService;
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

}
