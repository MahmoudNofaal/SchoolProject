using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SchoolProject.Application.Services.Abstractions;
using SchoolProject.Domain.Entities.Authentication;
using SchoolProject.Domain.Entities.Identity;
using SchoolProject.Domain.Helpers;
using SchoolProject.Domain.Results;
using SchoolProject.Infrastructure.Repositories.Abstractionss;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace SchoolProject.Application.Services;

public class AuthenticationService : IAuthenticationService
{
   private readonly JwtSettings _jwtSettings;
   private readonly IRefreshTokenRepository _refreshTokenRepository;
   private readonly UserManager<ApplicationUser> _userManager;

   public AuthenticationService(JwtSettings jwtSettings,
                                IRefreshTokenRepository refreshTokenRepository,
                                UserManager<ApplicationUser> userManager)
   {
      this._jwtSettings = jwtSettings;
      this._refreshTokenRepository = refreshTokenRepository;
      this._userManager = userManager;
   }


   #region Private Helper Methods
   private async Task<(JwtSecurityToken, string)> GenerateAccessToken(ApplicationUser user)
   {
      var claims = await this.GetUserClaims(user);

      var encodedKey = Encoding.UTF8.GetBytes(_jwtSettings.Secret);

      var key = new SymmetricSecurityKey(encodedKey);
      var algorithm = SecurityAlgorithms.HmacSha256;

      var signInCredentials = new SigningCredentials(key, algorithm);

      var jwtToken = new JwtSecurityToken
      (
         issuer: _jwtSettings.Issuer,
         audience: _jwtSettings.Audience,
         claims: claims,
         expires: DateTime.UtcNow.AddDays(_jwtSettings.AccessTokenExpireDateInDay),
         signingCredentials: signInCredentials
      );

      var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);

      return (jwtToken, accessToken);
   }

   private async Task<List<Claim>> GetUserClaims(ApplicationUser user)
   {
      var roles = await _userManager.GetRolesAsync(user);

      var claims = new List<Claim>()
      {
         new Claim((ClaimTypes.NameIdentifier), user.Id.ToString()),
         new Claim(nameof(UserClaimModel.Id), user.Id.ToString()),
         new Claim((ClaimTypes.Name), user.UserName),
         new Claim((ClaimTypes.Email), user.Email),
         new Claim((ClaimTypes.MobilePhone), user.PhoneNumber),
      };

      foreach (var item in roles)
      {
         claims.Add(new Claim((ClaimTypes.Role), item));
      }

      return claims;
   }

   private string GetRefreshToken()
   {
      var randomNumber = new byte[32];

      var randomNumberGenerator = RandomNumberGenerator.Create();

      randomNumberGenerator.GetBytes(randomNumber);

      return Convert.ToBase64String(randomNumber);
   }

   private RefreshToken GenerateRefreshToken(ApplicationUser user)
   {
      var refreshToken = new RefreshToken()
      {
         UserId = user.Id,
         UserEmail = user.Email ?? "",
         TokenString = this.GetRefreshToken(),
         ExpiresAt = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpireDateInDay)
      };

      return refreshToken;
   } 
   #endregion

   #region Implementaion Methods From Interfaces
   public async Task<JwtAuthenticationResult> GenerateJWTTokenAsync(ApplicationUser user)
   {
      // Another way of using tuple
      //var jwtToken = this.GenerateAccessToken(user).Item1;
      //var accessToken = this.GenerateAccessToken(user).Item2;

      // way 2 to use tuple
      var (jwtToken, accessToken) = await this.GenerateAccessToken(user);

      var refreshToken = this.GenerateRefreshToken(user);

      // save to db first
      var userRefreshToken = new UserRefreshToken()
      {
         UserId = user.Id,
         AccessToken = accessToken,
         RefreshToken = refreshToken.TokenString,
         JwtId = jwtToken.Id,
         IsUsed = true,
         IsRevoked = false,
         AddedDate = DateTime.UtcNow,
         ExpiryDate = refreshToken.ExpiresAt,
      };

      await _refreshTokenRepository.AddAsync(userRefreshToken);

      refreshToken.Id = userRefreshToken.Id;

      var jwtAuthenticationResult = new JwtAuthenticationResult()
      {
         AccessToken = accessToken,
         RefreshToken = refreshToken
      };

      return jwtAuthenticationResult;
   }


   public async Task<JwtAuthenticationResult> RefreshTokenAsync
      (ApplicationUser user, UserRefreshToken userRefreshToken)
   {
      // Generate RefreshToken
      var newAccessToken = await this.GenerateAccessToken(user);

      userRefreshToken.AccessToken = newAccessToken.Item2;
      await _refreshTokenRepository.UpdateAsync(userRefreshToken);

      return new JwtAuthenticationResult()
      {
         AccessToken = userRefreshToken.AccessToken,
         RefreshToken = new RefreshToken()
         {
            UserId = user.Id,
            UserEmail = user.Email,
            TokenString = userRefreshToken.RefreshToken,
            ExpiresAt = userRefreshToken.ExpiryDate
         }
      };
   }

   public Task<JwtSecurityToken> DecodeAccessTokenAsync(string accessToken)
   {
      if (string.IsNullOrEmpty(accessToken))
      {
         throw new ArgumentNullException(nameof(accessToken));
      }

      var handler = new JwtSecurityTokenHandler();

      return Task.FromResult(handler.ReadJwtToken(accessToken));
   }

   public async Task<string> ValidateAccessToken(string accessToken)
   {
      if (string.IsNullOrEmpty(accessToken))
      {
         throw new ArgumentNullException(nameof(accessToken));
      }

      var handler = new JwtSecurityTokenHandler();

      var tokenParameters = new TokenValidationParameters
      {
         ValidateIssuer = _jwtSettings.ValidateIssuer,
         ValidIssuers = new[] { _jwtSettings.Issuer },
         ValidateIssuerSigningKey = _jwtSettings.ValidateIssuerSigninKey,
         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret)),
         ValidateAudience = _jwtSettings.ValidateAudience,
         ValidAudience = _jwtSettings.Audience,
         ValidateLifetime = _jwtSettings.ValidateLifetime
      };

      var validator = handler.ValidateToken(accessToken, tokenParameters, out SecurityToken validatedToken);

      try
      {
         if (validator is null)
         {
            throw new SecurityTokenException("Invalid Token!");
         }

         return "Success";
      }
      catch (Exception ex)
      {
         return ex.Message;
      }
   }

   public async Task<(string, UserRefreshToken)> ValidateDetailsAsync(JwtSecurityToken jwtSecurityToken, string accessToken, string refreshToken)
   {
      // check token
      if (jwtSecurityToken is null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256))
      {
         return ("InvalidToken", null);
      }

      // check the expiryDate of accessToken
      if (jwtSecurityToken.ValidTo > DateTime.UtcNow)
      {
         return ("TokenIsNotExpired", null);
      }

      // get Claim Email
      var userId = jwtSecurityToken.Claims.FirstOrDefault(x => x.Type == nameof(UserClaimModel.Id)).Value;

      // Get User
      var userRefreshToken = await _refreshTokenRepository.GetTableAsNoTracking()
                                                          .FirstOrDefaultAsync(x => x.UserId == int.Parse(userId) &&
                                                                                    x.AccessToken == accessToken &&
                                                                                    x.RefreshToken == refreshToken);

      if (userRefreshToken is null)
      {
         return ("InvalidAccessTokenOrRefreshToken", null);
      }

      // check the expiryDate of refreshToken
      if (userRefreshToken.ExpiryDate < DateTime.UtcNow)
      {
         userRefreshToken.IsRevoked = true;
         userRefreshToken.IsUsed = false;

         await _refreshTokenRepository.UpdateAsync(userRefreshToken);

         return ("RefreshTokenIsExpired", null);
      }

      // validate completed
      return (userId, userRefreshToken);
   }


   #endregion

}
