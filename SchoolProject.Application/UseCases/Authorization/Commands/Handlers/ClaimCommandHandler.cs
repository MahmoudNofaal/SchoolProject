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

internal class ClaimCommandHandler : ResponseHandler,
                                     ICommandHandler<UpdateUserClaimsCommand, Response<string>>
{
   private readonly UserManager<ApplicationUser> _userManager;
   private readonly IAuthorizationService _authorizationService;

   public ClaimCommandHandler(IStringLocalizer<SharedResources> stringLocalizer,
                              UserManager<ApplicationUser> userManager,
                              IAuthorizationService authorizationService) : base(stringLocalizer)
   {
      this._userManager = userManager;
      this._authorizationService = authorizationService;
   }


   #region Update User Claims - Handle
   public async Task<Response<string>> Handle(UpdateUserClaimsCommand request, CancellationToken cancellationToken)
   {
      var user = await _userManager.FindByIdAsync(request.UserId.ToString());

      if (user == null)
      {
         return NotFound<string>();
      }

      var result = await _authorizationService.UpdateUserClaimsAsync(user, request.Claims.ToList());

      if (result == "Success")
      {
         return Success("User claims updated successfully.");
      }

      return BadRequest<string>(result);
   }
   #endregion

}
