using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.Application.Services.Abstractions;
using SchoolProject.Domain.Entities.Identity;
using SchoolProject.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Application.Services;

public class ApplicationUserService : IApplicationUserService
{
   private readonly UserManager<ApplicationUser> _userManager;
   private readonly IHttpContextAccessor _httpContextAccessor;
   private readonly IUrlHelper _urlHelper;
   private readonly IEmailService _emailService;
   private readonly ApplicationDbContext _dbContext;

   public ApplicationUserService(UserManager<ApplicationUser> userManager,
                                 IHttpContextAccessor httpContextAccessor,
                                 IUrlHelper urlHelper,
                                 IEmailService emailService,
                                 ApplicationDbContext dbContext)
   {
      this._userManager = userManager;
      this._httpContextAccessor = httpContextAccessor;
      this._urlHelper = urlHelper;
      this._emailService = emailService;
      this._dbContext = dbContext;
   }

   #region Create User
   public async Task<string> CreateUserAsync(ApplicationUser user, string password)
   {
      using var trans = await _dbContext.Database.BeginTransactionAsync();

      try
      {
         var resultOfCreate = await _userManager.CreateAsync(user, password);

         // Check result if true
         if (!resultOfCreate.Succeeded)
         {
            return "Failed";
         }

         // Assign User to User Role
         var assignRoleResult = await _userManager.AddToRoleAsync(user, "User");
         if (!assignRoleResult.Succeeded)
         {
            return "Failed";
         }

         // 6. Send Email Confirmation
         var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);

         var resquestAccessor = _httpContextAccessor.HttpContext.Request;

         //$"/api/v1/authentication/confirmemail?userId={user.Id}&code={code}";
         var returnUrl = resquestAccessor.Scheme + "://" + resquestAccessor.Host + _urlHelper.Action("ConfirmEmail", "Authentication", new { userId = user.Id, code = code });

         var message = $"To Confirm Email Click Link: <a href='{returnUrl}'>Link Of Confirmation</a>";

         var sendEmailResult = await _emailService.SendEmailAsync(user.Email, message, "Confirm email");

         if (sendEmailResult != "Success")
         {
            return "Failed";
         }

         await trans.CommitAsync();

         return "Success";
      } //end of try
      catch
      {
         await trans.RollbackAsync();
         return "Failed";
      }
      // end of using

   }
   #endregion

}
