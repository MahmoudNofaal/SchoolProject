using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using SchoolProject.Application.Bases;
using SchoolProject.Application.Bases.CQRS;
using SchoolProject.Application.Features.Authentication.Commands.Models;
using SchoolProject.Application.Resources;
using SchoolProject.Application.Services.Abstractions;
using SchoolProject.Domain.Entities.Identity;
using SchoolProject.Domain.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolProject.Application.Features.Authentication.Commands.Handlers;

public class AuthenticationCommandHandler : ResponseHandler,
                                            ICommandHandler<SignInCommand, Response<JwtAuthenticationResult>>,
                                            ICommandHandler<RefreshTokenCommand, Response<JwtAuthenticationResult>>
{
   private readonly UserManager<ApplicationUser> _userManager;
   private readonly SignInManager<ApplicationUser> _signInManager;
   private readonly IAuthenticationService _authenticationService;

   public AuthenticationCommandHandler(IStringLocalizer<SharedResources> stringLocalizer,
                                       UserManager<ApplicationUser> userManager,
                                       SignInManager<ApplicationUser> signInManager,
                                       IAuthenticationService authenticationService) : base(stringLocalizer)
   {
      this._userManager = userManager;
      this._signInManager = signInManager;
      this._authenticationService = authenticationService;
   }

   #region Sign In - Handle
   public async Task<Response<JwtAuthenticationResult>> Handle(SignInCommand request, CancellationToken cancellationToken)
   {
      // 1. check if user exists or not
      var user = await _userManager.FindByEmailAsync(request.Email);

      // 2. return notfound
      if (user == null)
      {
         return NotFound<JwtAuthenticationResult>();
      }

      // 3. try to sign user in
      var checkPasswordresult = await _signInManager.CheckPasswordSignInAsync(user, request.Password, true);

      // 4. check the result of sign in
      if (!checkPasswordresult.Succeeded)
      {
         return BadRequest<JwtAuthenticationResult>();
      }

      // 6. generate token
      var resultToken = await _authenticationService.GenerateJWTTokenAsync(user);

      // 7. return token
      return Success(resultToken);
   }
   #endregion


   #region REfresh Token - Handle
   public async Task<Response<JwtAuthenticationResult>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
   {
      var decodedAccessTokenResult = await _authenticationService.DecodeAccessTokenAsync(request.AcessToken);

      var validateResult_userId = await _authenticationService.ValidateDetailsAsync(decodedAccessTokenResult, request.AcessToken, request.RefreshToken);

      switch (validateResult_userId.Item1)
      {
         case "InvalidToken":
         case "TokenIsNotExpired":
         case "InvalidAccessTokenOrRefreshToken":
         case "RefreshTokenIsExpired":
            return Unauthorized<JwtAuthenticationResult>(validateResult_userId.Item1);
      }

      var user = await _userManager.FindByIdAsync(validateResult_userId.Item1);

      if (user is null)
      {
         return NotFound<JwtAuthenticationResult>("User not found!");
      }

      var result = await _authenticationService.RefreshTokenAsync(user, validateResult_userId.Item2);

      return Success(result);
   }
   #endregion

}
