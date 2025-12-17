using Microsoft.Extensions.Localization;
using SchoolProject.Application.Bases;
using SchoolProject.Application.Bases.CQRS;
using SchoolProject.Application.Resources;
using SchoolProject.Application.Services.Abstractions;
using SchoolProject.Application.UseCases.Emails.Commands.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Application.UseCases.Emails.Commands.Handlers;

public class EmailCommandHandler : ResponseHandler,
                                   ICommandHandler<SendEmailCommand, Response<string>>
{
   private readonly IEmailService _emailService;

   public EmailCommandHandler(IStringLocalizer<SharedResources> stringLocalizer,
                              IEmailService emailService) : base(stringLocalizer)
   {
      this._emailService = emailService;
   }

   #region Send Email - Handle
   public async Task<Response<string>> Handle(SendEmailCommand request, CancellationToken cancellationToken)
   {
      var response = await _emailService.SendEmailAsync(request.UserEmail, request.Message, null);

      if (response == "Success")
      {
         return Success("Email sent successfully");
      }

      return BadRequest<string>();
   } 
   #endregion

}
