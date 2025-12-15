using Microsoft.Extensions.Localization;
using SchoolProject.Application.Bases;
using SchoolProject.Application.Bases.CQRS;
using SchoolProject.Application.Resources;
using SchoolProject.Application.Services.Abstractions;
using SchoolProject.Application.UseCases.Authentication.Queries.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Application.UseCases.Authentication.Queries.Handlers;

public class AuthenticationUserQueryHandler : ResponseHandler,
                                              IQueryHandler<AuthorizeUserQuery, Response<string>>
{
   private readonly IAuthenticationService _authenticationService;

   public AuthenticationUserQueryHandler(IStringLocalizer<SharedResources> stringLocalizer,
                                         IAuthenticationService authenticationService) : base(stringLocalizer)
   {
      this._authenticationService = authenticationService;
   }


   #region Authroize User - Handle
   public async Task<Response<string>> Handle(AuthorizeUserQuery request, CancellationToken cancellationToken)
   {
      var result = await _authenticationService.ValidateAccessToken(request.AccessToken);

      if (result == "Success")
      {
         return Success("Token is valid!");
      }

      return BadRequest<string>("Token is not valid!");
   } 
   #endregion

}
