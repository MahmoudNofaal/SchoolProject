using SchoolProject.Domain.Entities.Authentication;
using SchoolProject.Domain.Entities.Identity;
using SchoolProject.Domain.Results;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace SchoolProject.Application.Services.Abstractions;

public interface IAuthenticationService
{
   Task<JwtSecurityToken> DecodeAccessTokenAsync(string accessToken);

   Task<JwtAuthenticationResult> GenerateJWTTokenAsync(ApplicationUser user);

   Task<(string, UserRefreshToken)> ValidateDetailsAsync(JwtSecurityToken jwtSecurityToken, string accessToken, string refreshToken);

   Task<JwtAuthenticationResult> RefreshTokenAsync(ApplicationUser user, UserRefreshToken userRefreshToken);

   Task<string> ValidateAccessToken(string accessToken);

}
