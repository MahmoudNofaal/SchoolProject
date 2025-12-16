using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using SchoolProject.Application.Bases;
using SchoolProject.Application.Bases.CQRS;
using SchoolProject.Application.Resources;
using SchoolProject.Application.Services.Abstractions;
using SchoolProject.Application.UseCases.Authorization.Queries.Models;
using SchoolProject.Application.UseCases.Authorization.Queries.Results;
using SchoolProject.Domain.Entities.Identity;
using SchoolProject.Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Application.UseCases.Authorization.Queries.Handlers;

public class ClaimQueryHandler : ResponseHandler,
                                 IQueryHandler<GetUserClaimsQuery, Response<UserClaimsResult>>
{
   private readonly IAuthorizationService _authorizationService;
   private readonly UserManager<ApplicationUser> _userManager;

   public ClaimQueryHandler(IStringLocalizer<SharedResources> stringLocalizer,
                            IAuthorizationService authorizationService,
                            UserManager<ApplicationUser> userManager) : base(stringLocalizer)
   {
      this._authorizationService = authorizationService;
      this._userManager = userManager;
   }

   #region Get User Claims - Handle
   public async Task<Response<UserClaimsResult>> Handle(GetUserClaimsQuery request, CancellationToken cancellationToken)
   {
      var user = await _userManager.FindByIdAsync(request.UserId.ToString());

      if (user is null)
      {
         return NotFound<UserClaimsResult>();
      }

      var result = await _authorizationService.GetClaimsAndUserClaimsAsync(user);

      return Success(result);
   } 
   #endregion
}
